using System.Text.Json;
using Blockchain.Application.Common.Errors;
using Blockchain.Application.Common.Logging;
using FluentResults;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed partial class FetchEthMainSnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repository,
    ILogger<FetchEthMainSnapshotHandler> logger,    
    IUnitOfWork uow)
    : IRequestHandler<FetchEthMainSnapshotCommand, Result>
{
    public async Task<Result> Handle(
        FetchEthMainSnapshotCommand request,
        CancellationToken cancellationToken)
    {
        var apiResult = await client.GetEthMainAsync(cancellationToken);

        if (apiResult.IsFailed)
        {
            LogBlockchainApiError(new (apiResult.Errors));
            return Result.Fail(new ExternalServiceUnavailableError("BlockCypher unreachable"));
        }

        var snapshot = new BlockchainSnapshot
        {
            Blockchain = BlockchainType.EthMain,
            CreatedAt = DateTime.UtcNow,
            PayloadJson = JsonSerializer.Serialize(apiResult.Value)
        };

        try
        {
            await repository.AddAsync(snapshot, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to save snapshot to database");
            return Result.Fail(new SnapshotStoreFailedError("Error saving snapshot"));
        }

        return Result.Ok();
    }
    
    [LoggerMessage(Level = LogLevel.Error, Message = "Blockchain API returned an error: {errors}")]
    private partial void LogBlockchainApiError(ErrorLogValues errors);
}
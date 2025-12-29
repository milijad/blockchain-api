using System.Text.Json;
using Blockchain.Application.Common.Errors;
using Blockchain.Application.Common.Logging;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal abstract partial class FetchBlockchainSnapshotHandler<TCommand, TResponse>(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repo,
    IUnitOfWork uow,
    ILogger logger)
    : IRequestHandler<TCommand, Result> where TCommand : IRequest<Result>
{
    protected abstract BlockchainType Blockchain { get; }
    protected abstract Func<IBlockCypherClient, CancellationToken, Task<Result<TResponse>>> ApiCall { get; }
    protected abstract string ErrorMessage { get; }

    public async Task<Result> Handle(TCommand _, CancellationToken ct)
    {
        var apiResult = await ApiCall(client, ct);

        if (apiResult.IsFailed)
        {
            LogBlockchainApiError(new(apiResult.Errors));
            return Result.Fail(new ExternalServiceUnavailableError(ErrorMessage));
        }

        try
        {
            await repo.AddAsync(new BlockchainSnapshot
            {
                Blockchain = Blockchain,
                CreatedAt = DateTime.UtcNow,
                PayloadJson = JsonSerializer.Serialize(apiResult.Value)
            }, ct);

            await uow.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to save snapshot to database");
            return Result.Fail(new SnapshotStoreFailedError("Error saving snapshot"));
        }

        return Result.Ok();
    }

    [LoggerMessage(Level = LogLevel.Error, Message = "Blockchain API returned an error: {errors}")]
    private partial void LogBlockchainApiError(ErrorLogValues errors);
}
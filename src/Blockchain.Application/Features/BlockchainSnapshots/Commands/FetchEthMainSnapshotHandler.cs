using System.Text.Json;
using Blockchain.Application.Common.Errors;
using FluentResults;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;
using MediatR;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed class FetchEthMainSnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repository,
    IUnitOfWork uow)
    : IRequestHandler<FetchEthMainSnapshotCommand, Result>
{
    public async Task<Result> Handle(
        FetchEthMainSnapshotCommand request,
        CancellationToken cancellationToken)
    {
        var apiResult = await client.GetEthMainAsync(cancellationToken);

        if (apiResult.IsFailed)
            return Result.Fail(new ExternalServiceUnavailableError("BlockCypher unreachable"));

        var snapshot = new BlockchainSnapshot
        {
            Blockchain = BlockchainType.EthMain,
            CreatedAt = DateTime.UtcNow,
            PayloadJson = JsonSerializer.Serialize(apiResult.Value)
        };

        await repository.AddAsync(snapshot, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
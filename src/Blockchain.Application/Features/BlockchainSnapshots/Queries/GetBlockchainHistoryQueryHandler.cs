using Blockchain.Application.Common.Errors;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Queries;

internal sealed class GetBlockchainHistoryQueryHandler(
    IBlockchainSnapshotRepository repository,
    ILogger<GetBlockchainHistoryQueryHandler> logger
    ) : IRequestHandler<GetBlockchainHistoryQuery, Result<IReadOnlyList<string>>>
{
    public async Task<Result<IReadOnlyList<string>>> Handle(
        GetBlockchainHistoryQuery request,
        CancellationToken ct)
    {
        IReadOnlyList<BlockchainSnapshot> items;
        try
        {
            items = await repository.GetHistoryAsync(request.Blockchain, request.Limit, ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting blockchain history");
            return Result.Fail(new SnapshotHistoryReadFailedError("Error getting history"));
        }

        var payloads = items.Select(x => x.PayloadJson).ToList();

        return Result.Ok<IReadOnlyList<string>>(payloads);
    }
}
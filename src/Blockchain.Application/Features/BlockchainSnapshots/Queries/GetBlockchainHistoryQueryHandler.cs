using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Enums;
using FluentResults;
using MediatR;

namespace Blockchain.Application.Features.BlockchainSnapshots.Queries;

internal sealed class GetBlockchainHistoryQueryHandler(IBlockchainSnapshotRepository repository)
    : IRequestHandler<GetBlockchainHistoryQuery, Result<IReadOnlyList<string>>>
{
    public async Task<Result<IReadOnlyList<string>>> Handle(
        GetBlockchainHistoryQuery request,
        CancellationToken ct)
    {
        var items = await repository.GetHistoryAsync(request.Blockchain, request.Limit, ct);

        var payloads = items.Select(x => x.PayloadJson).ToList();

        return Result.Ok<IReadOnlyList<string>>(payloads);
    }
}
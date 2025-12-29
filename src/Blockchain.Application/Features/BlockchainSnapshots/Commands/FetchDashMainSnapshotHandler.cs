using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed class FetchDashMainSnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repo,
    IUnitOfWork uow,
    ILogger<FetchDashMainSnapshotHandler> logger)
    : FetchBlockchainSnapshotHandler<FetchDashMainSnapshotCommand, DashMainResponseDto>(client, repo, uow, logger)
{
    protected override BlockchainType Blockchain => BlockchainType.DashMain;
    protected override string ErrorMessage => "DASH API unreachable";
    protected override Func<IBlockCypherClient, CancellationToken, Task<Result<DashMainResponseDto>>> ApiCall
        => (c, ct) => c.GetDashMainAsync(ct);
}

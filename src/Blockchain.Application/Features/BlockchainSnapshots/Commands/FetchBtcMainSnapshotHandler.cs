using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed class FetchBtcMainSnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repo,
    IUnitOfWork uow,
    ILogger<FetchBtcMainSnapshotHandler> logger)
    : FetchBlockchainSnapshotHandler<FetchBtcMainSnapshotCommand, BtcMainResponseDto>(client, repo, uow, logger)
{
    protected override BlockchainType Blockchain => BlockchainType.BtcMain;
    protected override string ErrorMessage => "BTC API unreachable";
    protected override Func<IBlockCypherClient, CancellationToken, Task<Result<BtcMainResponseDto>>> ApiCall
        => (c, ct) => c.GetBtcMainAsync(ct);
}
using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed class FetchBtcTest3SnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repo,
    IUnitOfWork uow,
    ILogger<FetchBtcTest3SnapshotHandler> logger)
    : FetchBlockchainSnapshotHandler<FetchBtcTest3SnapshotCommand, BtcTest3ResponseDto>(client, repo, uow, logger)
{
    protected override BlockchainType Blockchain => BlockchainType.BtcTest3;
    protected override string ErrorMessage => "BTC Test3 API unreachable";
    protected override Func<IBlockCypherClient, CancellationToken, Task<Result<BtcTest3ResponseDto>>> ApiCall
        => (c, ct) => c.GetBtcTest3Async(ct);
}
using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed class FetchLtcMainSnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repo,
    IUnitOfWork uow,
    ILogger<FetchLtcMainSnapshotHandler> logger)
    : FetchBlockchainSnapshotHandler<FetchLtcMainSnapshotCommand, LtcMainResponseDto>(client, repo, uow, logger)
{
    protected override BlockchainType Blockchain => BlockchainType.LtcMain;
    protected override string ErrorMessage => "LTC API unreachable";
    protected override Func<IBlockCypherClient, CancellationToken, Task<Result<LtcMainResponseDto>>> ApiCall
        => (c, ct) => c.GetLtcMainAsync(ct);
}
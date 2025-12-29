using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Blockchain.Application.Features.BlockchainSnapshots.Commands;

internal sealed class FetchEthMainSnapshotHandler(
    IBlockCypherClient client,
    IBlockchainSnapshotRepository repo,
    IUnitOfWork uow,
    ILogger<FetchEthMainSnapshotHandler> logger)
    : FetchBlockchainSnapshotHandler<FetchEthMainSnapshotCommand, EthMainResponseDto>(client, repo, uow, logger)
{
    protected override BlockchainType Blockchain => BlockchainType.EthMain;
    protected override string ErrorMessage => "ETH API unreachable";
    protected override Func<IBlockCypherClient, CancellationToken, Task<Result<EthMainResponseDto>>> ApiCall
        => (c, ct) => c.GetEthMainAsync(ct);
}
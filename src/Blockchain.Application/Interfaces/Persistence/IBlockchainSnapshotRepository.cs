using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;

namespace Blockchain.Application.Interfaces.Persistence;

public interface IBlockchainSnapshotRepository
{
    Task AddAsync(BlockchainSnapshot snapshot, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlockchainSnapshot>> GetHistoryAsync(
        BlockchainType type,
        int limit,
        CancellationToken cancellationToken);
}   
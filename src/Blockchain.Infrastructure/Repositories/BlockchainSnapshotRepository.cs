using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;
using Blockchain.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Infrastructure.Repositories;

internal sealed class BlockchainSnapshotRepository(AppDbContext context) : IBlockchainSnapshotRepository
{
    public async Task AddAsync(BlockchainSnapshot snapshot, CancellationToken cancellationToken)
    {
        await context.BlockchainSnapshots.AddAsync(snapshot, cancellationToken);
    }

    public async Task<IReadOnlyList<BlockchainSnapshot>> GetHistoryAsync(
        BlockchainType type,
        int limit,
        CancellationToken ct)
    {
        return await context.BlockchainSnapshots
            .Where(x => x.Blockchain == type)
            .OrderByDescending(x => x.Id)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
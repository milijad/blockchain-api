using Blockchain.Domain.Entities;
using Blockchain.Domain.Enums;
using Blockchain.Infrastructure.Persistence;
using Blockchain.Infrastructure.Repositories;
using Blockchain.TestInfrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.IntegrationTests;

public class BlockchainSnapshotRepositoryTests(PostgresFixture fixture) : IClassFixture<PostgresFixture>
{
    [Fact]
    public async Task CanInsertAndReadSnapshot()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(fixture.ConnectionString)
            .Options;

        await using var ctx = new AppDbContext(options);

        var repo = new BlockchainSnapshotRepository(ctx);

        await repo.AddAsync(new BlockchainSnapshot
        {
            Blockchain = BlockchainType.EthMain,
            CreatedAt = DateTime.UtcNow,
            PayloadJson = "{}"
        }, CancellationToken.None);

        await ctx.SaveChangesAsync();

        var history = await repo.GetHistoryAsync(BlockchainType.EthMain, 10, CancellationToken.None);

        history.Should().HaveCount(1);
    }
}

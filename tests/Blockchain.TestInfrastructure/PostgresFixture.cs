using Blockchain.Infrastructure.Persistence;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Configurations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Blockchain.TestInfrastructure;

public sealed class PostgresFixture : IAsyncLifetime
{
    private readonly PostgreSqlTestcontainer _container;

    public string ConnectionString => _container.ConnectionString;

    public PostgresFixture()
    {
        _container = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "blockchain",
                Username = "test",
                Password = "test"
            })
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        await using var ctx = new AppDbContext(options);
        await ctx.Database.MigrateAsync(); 
    }
    
    public Task DisposeAsync() => _container.DisposeAsync().AsTask();
}
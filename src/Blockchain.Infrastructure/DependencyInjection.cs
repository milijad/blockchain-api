using Blockchain.Application.Common;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Application.Interfaces.Persistence;
using Blockchain.Infrastructure.Clients;
using Blockchain.Infrastructure.Configuration;
using Blockchain.Infrastructure.Persistence;
using Blockchain.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blockchain.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlockCypherOptions>(
            configuration.GetSection(BlockCypherConstants.ConfigurationSection));

        services.AddHttpClient<IBlockCypherClient, BlockCypherClient>();
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(ConfigurationKeys.PostgresConnection)));
        
        services.AddScoped<IBlockchainSnapshotRepository, BlockchainSnapshotRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
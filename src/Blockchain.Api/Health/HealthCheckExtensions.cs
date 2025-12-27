using Blockchain.Api.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blockchain.Api.Health;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddApplicationHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Postgres") ?? string.Empty)
            .AddCheck<BlockCypherHealthCheck>("blockcypher");

        return services;
    }

    public static WebApplication MapApplicationHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health/live", new()
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/ready");

        return app;
    }
}
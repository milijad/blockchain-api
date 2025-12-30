using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Blockchain.Infrastructure.Persistence;

namespace Blockchain.Api.Health;

public sealed class DatabaseSchemaHealthCheck(AppDbContext db) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            const string sql =
                """SELECT 1 FROM "BlockchainSnapshots" LIMIT 1;""";

            await db.Database.ExecuteSqlRawAsync(sql, cancellationToken);
            return HealthCheckResult.Healthy("Schema is ready");
        }
        catch
        {
            return HealthCheckResult.Unhealthy("Database schema not ready");
        }
    }
}
using Blockchain.Application.Common;
using Blockchain.Infrastructure.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Blockchain.Api.Health;

public sealed class BlockCypherHealthCheck(
    IHttpClientFactory factory,
    IOptions<BlockCypherOptions> options) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var client = factory.CreateClient();
            client.BaseAddress = new Uri(options.Value.BaseUrl);

            var response = await client.GetAsync(BlockCypherConstants.EthMainEndpoint, cancellationToken);

            return response.IsSuccessStatusCode
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy("BlockCypher unreachable");
        }
        catch
        {
            return HealthCheckResult.Unhealthy("BlockCypher unreachable");
        }
    }
}
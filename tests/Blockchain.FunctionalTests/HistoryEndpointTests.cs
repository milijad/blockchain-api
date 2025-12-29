using Blockchain.TestInfrastructure;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Blockchain.FunctionalTests;

public class HistoryEndpointTests : IClassFixture<PostgresFixture>
{
    private readonly HttpClient _client;

    public HistoryEndpointTests(PostgresFixture fixture)
    {
        var factory = new CustomWebApplicationFactory(fixture);
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task HistoryEndpoint_Returns200()
    {
        var response = await _client.GetAsync("/api/blockchain/EthMain/history?limit=1");
        response.EnsureSuccessStatusCode();
    }
}
using Blockchain.Infrastructure.Clients;
using Blockchain.Infrastructure.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Blockchain.IntegrationTests;

public class BlockCypherClientIntegrationTests
{
    [Fact]
    public async Task GetEthMainAsync_ReturnsRealSnapshot()
    {
        var options = Options.Create(new BlockCypherOptions
        {
            BaseUrl = "https://api.blockcypher.com/"
        });

        using var http = new HttpClient();
        http.BaseAddress = new Uri(options.Value.BaseUrl);

        var client = new BlockCypherClient(http,NullLogger<BlockCypherClient>.Instance,  options);

        var result = await client.GetEthMainAsync(CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Contain("ETH.main");
    }
}
using Blockchain.Infrastructure.Clients;
using Blockchain.Infrastructure.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Blockchain.IntegrationTests;

[Trait("Category", "External")]
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

        var ethResult = await client.GetEthMainAsync(CancellationToken.None);
        ethResult.IsSuccess.Should().BeTrue();
        ethResult.Value.Name.Should().Contain("ETH.main");
        
        var btcMainResult = await client.GetBtcMainAsync(CancellationToken.None);
        btcMainResult.IsSuccess.Should().BeTrue();
        btcMainResult.Value.Name.Should().Contain("BTC.main");
        
        var btcTest3Result = await client.GetBtcTest3Async(CancellationToken.None);
        btcTest3Result.IsSuccess.Should().BeTrue();
        btcTest3Result.Value.Name.Should().Contain("BTC.test3");
        
        var dashResult = await client.GetDashMainAsync(CancellationToken.None);
        dashResult.IsSuccess.Should().BeTrue();
        dashResult.Value.Name.Should().Contain("DASH.main");
        
        var ltcResult = await client.GetLtcMainAsync(CancellationToken.None);
        ltcResult.IsSuccess.Should().BeTrue();
        ltcResult.Value.Name.Should().Contain("LTC.main");
    }
}
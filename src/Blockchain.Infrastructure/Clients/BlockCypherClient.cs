using System.Net.Http.Json;
using FluentResults;
using Blockchain.Application.Common;
using Blockchain.Application.DTOs.BlockCypher;
using Blockchain.Application.Interfaces.Clients;
using Blockchain.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blockchain.Infrastructure.Clients;

internal sealed class BlockCypherClient : IBlockCypherClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BlockCypherClient> _logger;

    public BlockCypherClient(
        HttpClient httpClient,
        ILogger<BlockCypherClient> logger,
        IOptions<BlockCypherOptions> options)
    {   
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    public async Task<Result<EthMainResponseDto>> GetEthMainAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Calling BlockCypher ETH endpoint");

            var response = await _httpClient.GetFromJsonAsync<EthMainResponseDto>(
                BlockCypherConstants.EthMainEndpoint,
                cancellationToken);

            if (response is not null) return Result.Ok(response);
            _logger.LogWarning("ETH response is null");
            return Result.Fail("Empty response from ETH main API");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ETH main request failed");
            return Result.Fail("BlockCypher ETH main call failed");
        }
    }
}
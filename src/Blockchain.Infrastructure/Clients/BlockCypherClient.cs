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

    public Task<Result<EthMainResponseDto>> GetEthMainAsync(CancellationToken ct) =>
        GetAsync<EthMainResponseDto>(
            BlockCypherConstants.EthMainEndpoint,
            "ETH.main",
            ct);
    
    public Task<Result<DashMainResponseDto>> GetDashMainAsync(CancellationToken ct) =>
        GetAsync<DashMainResponseDto>(
            BlockCypherConstants.DashMainEndpoint,
            "DASH.main",
            ct);
    
    public Task<Result<BtcMainResponseDto>> GetBtcMainAsync(CancellationToken ct) =>
        GetAsync<BtcMainResponseDto>(
            BlockCypherConstants.BtcMainEndpoint,
            "BTC.main",
            ct);
    
    public Task<Result<BtcTest3ResponseDto>> GetBtcTest3Async(CancellationToken ct) =>
        GetAsync<BtcTest3ResponseDto>(
            BlockCypherConstants.BtcTest3Endpoint,
            "BTC.test3",
            ct);
    
    public Task<Result<LtcMainResponseDto>> GetLtcMainAsync(CancellationToken ct) =>
        GetAsync<LtcMainResponseDto>(
            BlockCypherConstants.LtcMainEndpoint,
            "LTC.main",
            ct);
    
    private async Task<Result<TResponse>> GetAsync<TResponse>(
        string endpoint,
        string blockchainName,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Calling BlockCypher {Blockchain} endpoint", blockchainName);

            var response = await _httpClient.GetFromJsonAsync<TResponse>(endpoint, cancellationToken);

            if (response is not null)
                return Result.Ok(response);

            _logger.LogWarning("{Blockchain} response is null", blockchainName);
            return Result.Fail($"Empty response from {blockchainName} API");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Blockchain} request failed", blockchainName);
            return Result.Fail($"BlockCypher {blockchainName} call failed");
        }
    }
}
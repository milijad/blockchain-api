using System.Text.Json.Serialization;

namespace Blockchain.Application.DTOs.BlockCypher;

public sealed class EthMainResponseDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    [JsonPropertyName("height")]
    public long Height { get; init; }

    [JsonPropertyName("hash")]
    public string Hash { get; init; } = null!;

    [JsonPropertyName("time")]
    public DateTime Time { get; init; }

    [JsonPropertyName("latest_url")]
    public string LatestUrl { get; init; } = null!;

    [JsonPropertyName("previous_hash")]
    public string PreviousHash { get; init; } = null!;

    [JsonPropertyName("previous_url")]
    public string PreviousUrl { get; init; } = null!;

    [JsonPropertyName("peer_count")]
    public int PeerCount { get; init; }

    [JsonPropertyName("unconfirmed_count")]
    public int UnconfirmedCount { get; init; }

    [JsonPropertyName("high_gas_price")]
    public long HighGasPrice { get; init; }

    [JsonPropertyName("medium_gas_price")]
    public long MediumGasPrice { get; init; }

    [JsonPropertyName("low_gas_price")]
    public long LowGasPrice { get; init; }

    [JsonPropertyName("high_priority_fee")]
    public long HighPriorityFee { get; init; }

    [JsonPropertyName("medium_priority_fee")]
    public long MediumPriorityFee { get; init; }

    [JsonPropertyName("low_priority_fee")]
    public long LowPriorityFee { get; init; }

    [JsonPropertyName("base_fee")]
    public long BaseFee { get; init; }

    [JsonPropertyName("last_fork_height")]
    public long LastForkHeight { get; init; }

    [JsonPropertyName("last_fork_hash")]
    public string LastForkHash { get; init; } = null!;
}
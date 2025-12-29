using System.Text.Json.Serialization;

namespace Blockchain.Application.DTOs.BlockCypher;

public sealed record DashMainResponseDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("height")]
    public long Height { get; init; }

    [JsonPropertyName("hash")]
    public string Hash { get; init; } = string.Empty;

    [JsonPropertyName("time")]
    public DateTime Time { get; init; }

    [JsonPropertyName("latest_url")]
    public string LatestUrl { get; init; } = string.Empty;

    [JsonPropertyName("previous_hash")]
    public string PreviousHash { get; init; } = string.Empty;

    [JsonPropertyName("previous_url")]
    public string PreviousUrl { get; init; } = string.Empty;

    [JsonPropertyName("peer_count")]
    public int PeerCount { get; init; }

    [JsonPropertyName("unconfirmed_count")]
    public int UnconfirmedCount { get; init; }

    [JsonPropertyName("high_fee_per_kb")]
    public long HighFeePerKb { get; init; }

    [JsonPropertyName("medium_fee_per_kb")]
    public long MediumFeePerKb { get; init; }

    [JsonPropertyName("low_fee_per_kb")]
    public long LowFeePerKb { get; init; }

    [JsonPropertyName("last_fork_height")]
    public long LastForkHeight { get; init; }

    [JsonPropertyName("last_fork_hash")]
    public string LastForkHash { get; init; } = string.Empty;
}
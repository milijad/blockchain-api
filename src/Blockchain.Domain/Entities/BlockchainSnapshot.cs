using Blockchain.Domain.Enums;

namespace Blockchain.Domain.Entities;

public sealed class BlockchainSnapshot
{
    public long Id { get; set; }
    public BlockchainType Blockchain { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PayloadJson { get; set; } = string.Empty;
}
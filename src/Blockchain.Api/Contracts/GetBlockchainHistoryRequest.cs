namespace Blockchain.Api.Contracts;

public sealed record GetBlockchainHistoryRequest(
    string Type,
    int Limit
    );
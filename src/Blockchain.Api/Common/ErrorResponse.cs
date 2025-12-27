namespace Blockchain.Api.Common;

public sealed record ErrorResponse(
    string Code,
    string Message,
    string? TraceId);
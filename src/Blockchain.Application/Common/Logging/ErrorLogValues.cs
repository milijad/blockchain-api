using FluentResults;

namespace Blockchain.Application.Common.Logging;

public readonly struct ErrorLogValues(IReadOnlyList<IError> errors)
{
    public override string ToString()
        => string.Join("; ", errors.Select(e => e.Message));
}
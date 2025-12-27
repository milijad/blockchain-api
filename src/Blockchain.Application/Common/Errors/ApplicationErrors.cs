using FluentResults;

namespace Blockchain.Application.Common.Errors;

public sealed class ExternalServiceUnavailableError(string message) : Error(message);

public sealed class ValidationFailedError(string message) : Error(message);

public sealed class NotFoundError(string message) : Error(message);
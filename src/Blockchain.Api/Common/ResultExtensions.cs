using Blockchain.Application.Common.Errors;
using FluentResults;

namespace Blockchain.Api.Common;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return MapErrors(result.Errors);
    }

    public static IResult ToHttpResult(this Result result)
    {
        if (result.IsSuccess)
            return Results.Ok();

        return MapErrors(result.Errors);
    }

    private static IResult MapErrors(IReadOnlyList<IError> errors)
    {
        var error = errors.FirstOrDefault() ?? new Error("Internal server error");

        return error switch
        {
            ExternalServiceUnavailableError e => Results.Problem(e.Message, statusCode: 503),
            ValidationFailedError e => Results.BadRequest(e.Message),
            NotFoundError e => Results.NotFound(e.Message),
            SnapshotStoreFailedError e => Results.Problem(e.Message, statusCode: 500),
            SnapshotHistoryReadFailedError e => Results.Problem(e.Message, statusCode: 500),
            _ => Results.Problem("Internal server error", statusCode: 500)
        };
    }
}
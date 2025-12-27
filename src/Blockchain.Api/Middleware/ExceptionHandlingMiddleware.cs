using System.Net;
using System.Text.Json;
using Blockchain.Api.Common;

namespace Blockchain.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse(
                "INTERNAL_ERROR",
                "Unexpected server error occurred",
                context.TraceIdentifier);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
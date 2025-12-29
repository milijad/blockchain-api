using Blockchain.Api.Common;
using Blockchain.Application.Features.BlockchainSnapshots.Commands;
using MediatR;

namespace Blockchain.Api.Endpoints;

public static class DashEndpoints
{
    public static IEndpointRouteBuilder MapDashEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blockchain/dash/fetch", async (
                    IMediator mediator,
                    CancellationToken ct) =>
                (await mediator.Send(new FetchDashMainSnapshotCommand(), ct)).ToHttpResult())
            .WithName("FetchDashMainSnapshot")
            .WithTags("Blockchain")
            .WithSummary("Fetch DASH mainnet snapshot")
            .WithDescription(
                "Calls BlockCypher DASH mainnet endpoint, stores full JSON payload in database")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status503ServiceUnavailable);

        return app;
    }
}
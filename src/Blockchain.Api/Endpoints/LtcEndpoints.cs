using Blockchain.Api.Common;
using Blockchain.Application.Features.BlockchainSnapshots.Commands;
using MediatR;

namespace Blockchain.Api.Endpoints;

public static class LtcEndpoints
{
    public static IEndpointRouteBuilder MapLtcEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blockchain/ltc/fetch", async (
                    IMediator mediator,
                    CancellationToken ct) =>
                (await mediator.Send(new FetchLtcMainSnapshotCommand(), ct)).ToHttpResult())
            .WithName("FetchLtcMainSnapshot")
            .WithTags("Blockchain")
            .WithSummary("Fetch LTC mainnet snapshot")
            .WithDescription(
                "Calls BlockCypher LTC mainnet endpoint, stores full JSON payload in database")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status503ServiceUnavailable);

        return app;
    }
}
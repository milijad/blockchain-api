using Blockchain.Api.Common;
using Blockchain.Application.Features.BlockchainSnapshots.Commands;
using MediatR;

namespace Blockchain.Api.Endpoints;

public static class BtcEndpoints
{
    public static IEndpointRouteBuilder MapBtcEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blockchain/btc/fetch", async (
                    IMediator mediator,
                    CancellationToken ct) =>
                (await mediator.Send(new FetchBtcMainSnapshotCommand(), ct)).ToHttpResult())
            .WithName("FetchBtcMainSnapshot")
            .WithTags("Blockchain")
            .WithSummary("Fetch BTC mainnet snapshot")
            .WithDescription(
                "Calls BlockCypher BTC mainnet endpoint, stores full JSON payload in database")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status503ServiceUnavailable);
        
        app.MapPost("/api/blockchain/btc-test3/fetch", async (
                    IMediator mediator,
                    CancellationToken ct) =>
                (await mediator.Send(new FetchBtcTest3SnapshotCommand(), ct)).ToHttpResult())
            .WithName("FetchBtcTest3Snapshot")
            .WithTags("Blockchain")
            .WithSummary("Fetch BTC testnet snapshot")
            .WithDescription(
                "Calls BlockCypher BTC testnet endpoint, stores full JSON payload in database")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status503ServiceUnavailable);

        return app;
    }
}
using Blockchain.Api.Common;
using Blockchain.Application.Features.BlockchainSnapshots.Commands;
using MediatR;

namespace Blockchain.Api.Endpoints;

public static class EthEndpoints
{
    public static IEndpointRouteBuilder MapEthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blockchain/eth/fetch", async (
                    IMediator mediator,
                    CancellationToken ct) =>
                (await mediator.Send(new FetchEthMainSnapshotCommand(), ct)).ToHttpResult())
            .WithName("FetchEthMainSnapshot")
            .WithTags("Blockchain")
            .WithSummary("Fetch ETH mainnet snapshot")
            .WithDescription(
                "Calls BlockCypher ETH mainnet endpoint, stores full JSON payload in database")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status503ServiceUnavailable);

        return app;
    }
}
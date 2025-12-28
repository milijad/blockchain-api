using Blockchain.Api.Common;
using Blockchain.Api.Contracts;
using Blockchain.Application.Features.BlockchainSnapshots.Commands;
using Blockchain.Application.Features.BlockchainSnapshots.Queries;
using Blockchain.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Blockchain.Api.Endpoints;

public static class BlockchainEndpoints
{
    public static void MapBlockchainEndpoints(this IEndpointRouteBuilder app)
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
        
        app.MapGet("/api/blockchain/{type}/history",
                async (
                    [AsParameters] GetBlockchainHistoryRequest request,
                    IValidator<GetBlockchainHistoryRequest> validator,
                    IMediator mediator, 
                    CancellationToken ct) =>
                {
                    var validation = await validator.ValidateAsync(request, ct);
                    if (!validation.IsValid)
                        return Results.BadRequest(validation.Errors);

                    var blockchain = Enum.Parse<BlockchainType>(request.Type, true);

                    var result = await mediator.Send(
                        new GetBlockchainHistoryQuery(blockchain, request.Limit), ct);

                    return result.ToHttpResult();
                })
            .WithName("GetBlockchainHistory")
            .WithTags("Blockchain")
            .WithSummary("Get blockchain snapshot history")
            .WithDescription(
                "Returns the latest stored blockchain snapshots.\n\n" +
                "**Query parameters:**\n" +
                "- `type` – Blockchain type (EthMain, BtcMain, BtcTest, DashMain, LtcMain)\n" +
                "- `limit` – Number of latest records to return (1–1000)")
            .Produces<IReadOnlyList<string>>()
            .Produces(StatusCodes.Status400BadRequest);
    }
}
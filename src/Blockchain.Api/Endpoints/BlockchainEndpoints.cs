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
            .WithTags("Blockchain");
        
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
            .WithTags("Blockchain");
    }
}
using Blockchain.Api.Endpoints;

namespace Blockchain.Api.Extensions;

public static class EndpointRegistrationExtensions
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapEthEndpoints();
        app.MapDashEndpoints();
        app.MapBtcEndpoints();
        app.MapLtcEndpoints();
        app.MapBlockchainHistoryEndpoints();

        return app;
    }
}

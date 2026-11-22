using EState.Api.Modules.Health;
using EState.Api.Modules.Listings;

namespace EState.Api.Startup;

internal static class EndpointRegistration
{
	public static void Map(IEndpointRouteBuilder app)
	{
		app.MapListingsEndpoints();
		app.MapHealthEndpoints();
	}
}

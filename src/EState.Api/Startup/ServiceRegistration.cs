using EState.Application.Modules.Listings;

namespace EState.Api.Startup;

internal static class ServiceRegistration
{
	public static void Register(IServiceCollection services)
	{
		services.AddOpenApi();
		services.AddScoped<ListingsService>();
	}
}

using EState.Application.Modules.Listings;

namespace EState.Api.Startup;

internal static class ServiceRegistration
{
	public static void RegisterApplicationServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddOpenApi();
		builder.Services.AddScoped<ListingsService>();
	}
}

using EState.Application.Modules.Listings;
using EState.Application.Modules.Listings.CreateListing;

namespace EState.Api.Startup;

internal static class ServiceRegistration
{
	public static void RegisterApplicationServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddOpenApi();
		builder.Services.AddValidation();

		builder.Services.ConfigureHttpJsonOptions(options =>
			options.SerializerOptions.RespectNullableAnnotations = true);

		builder.Services.AddScoped<ListingsService>();
		builder.Services.AddScoped<CreateListingHandler>();
	}
}

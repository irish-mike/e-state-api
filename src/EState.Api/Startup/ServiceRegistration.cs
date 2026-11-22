using System.Text.Json;
using System.Text.Json.Serialization;
using EState.Application.Modules.Listings;
using EState.Application.Modules.Listings.CreateListing;

namespace EState.Api.Startup;

internal static class ServiceRegistration
{
	public static void RegisterApplicationServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddOpenApi(options =>
			options.AddSchemaTransformer(ApiDocumentationConfiguration.ApplyEnumValues));
		builder.Services.AddValidation();

		builder.Services.ConfigureHttpJsonOptions(options =>
		{
			options.SerializerOptions.RespectNullableAnnotations = true;
			options.SerializerOptions.Converters.Add(
				new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, allowIntegerValues: false));
		});

		builder.Services.AddScoped<ListingsService>();
		builder.Services.AddScoped<CreateListingHandler>();
	}
}

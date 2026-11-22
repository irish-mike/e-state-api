using System.Reflection;
using System.Text.Json.Nodes;
using EState.Api.Shared.Validation;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace EState.Api.Startup;

internal static class ApiDocumentationConfiguration
{
	public static Task ApplyEnumValues(
		OpenApiSchema schema,
		OpenApiSchemaTransformerContext context,
		CancellationToken _)
	{
		var property = context.JsonPropertyInfo?.AttributeProvider as PropertyInfo;
		if (property?.GetCustomAttribute<EnumValueAttribute>() is { } enumValues)
			SetAllowedEnumValues(schema, enumValues);

		return Task.CompletedTask;
	}

	private static void SetAllowedEnumValues(OpenApiSchema schema, EnumValueAttribute enumValues)
	{
		List<JsonNode> names = [.. enumValues.Names.Select(name => JsonValue.Create(name))];
		if (schema.Type?.HasFlag(JsonSchemaType.Null) != true)
		{
			schema.Enum = names;
			return;
		}

		schema.AnyOf =
		[
			new OpenApiSchema { Type = JsonSchemaType.String, Enum = names },
			new OpenApiSchema { Type = JsonSchemaType.Null }
		];
	}

	public static void Configure(WebApplication app)
	{
		if (!app.Environment.IsDevelopment())
			return;

		app.MapOpenApi();
		ConfigureSwaggerUi(app);
		app.Lifetime.ApplicationStarted.Register(() => LogDocumentationUrls(app));
	}

	private static void ConfigureSwaggerUi(WebApplication app)
	{
		app.UseSwaggerUI(options =>
		{
			options.DocumentTitle = "e-state API";
			options.SwaggerEndpoint("../openapi/v1.json", "e-state API");
		});
	}

	private static void LogDocumentationUrls(WebApplication app)
	{
		foreach (var address in app.Urls)
		{
			var docsUrl = new UriBuilder(address) { Path = "/swagger" };
			if (docsUrl.Host is "0.0.0.0" or "[::]")
			{
				docsUrl.Host = "localhost";
			}

			app.Logger.LogInformation("API docs: {DocsUrl}", docsUrl.Uri);
		}
	}
}

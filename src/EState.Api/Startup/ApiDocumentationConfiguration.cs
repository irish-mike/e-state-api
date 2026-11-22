namespace EState.Api.Startup;

internal static class ApiDocumentationConfiguration
{
	public static void Configure(WebApplication app)
	{
		if (!app.Environment.IsDevelopment())
		{
			return;
		}

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

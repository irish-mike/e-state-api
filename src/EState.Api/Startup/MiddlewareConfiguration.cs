namespace EState.Api.Startup;

internal static class MiddlewareConfiguration
{
	public static void ConfigureMiddleware(this WebApplication app)
	{
		ApiDocumentationConfiguration.Configure(app);
		app.UseHttpsRedirection();
	}
}

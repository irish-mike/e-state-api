using EState.Infrastructure;

namespace EState.Api.Startup;

internal static class ApplicationBootstrapper
{
	public static WebApplication Build(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.RegisterApplicationServices();
		builder.Services.AddInfrastructure(builder.Configuration);

		var app = builder.Build();

		app.ConfigureMiddleware();
		app.ConfigureEndpoints();

		return app;
	}
}

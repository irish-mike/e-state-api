using EState.Infrastructure;

namespace EState.Api.Startup;

internal static class ApiStartup
{
	public static WebApplication Build(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		ServiceRegistration.Register(builder.Services);
		builder.Services.AddInfrastructure(builder.Configuration);

		var app = builder.Build();

		ApiDocumentationConfiguration.Configure(app);
		MiddlewareConfiguration.Configure(app);
		EndpointRegistration.Map(app);

		return app;
	}
}

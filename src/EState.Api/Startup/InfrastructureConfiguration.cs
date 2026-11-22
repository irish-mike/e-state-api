using EState.Infrastructure;

namespace EState.Api.Startup;

internal static class InfrastructureConfiguration
{
	public static void ConfigureInfrastructure(this WebApplicationBuilder builder)
	{
		builder.Services.AddInfrastructure(builder.Configuration);
	}
}

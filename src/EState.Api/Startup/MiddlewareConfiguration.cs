namespace EState.Api.Startup;

internal static class MiddlewareConfiguration
{
    public static void Configure(WebApplication app)
    {
        app.UseHttpsRedirection();
    }
}

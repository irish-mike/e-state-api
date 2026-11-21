using EState.Infrastructure.Persistence;

namespace EState.Api.Modules.Health;

internal static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/health", GetApiHealth);
        app.MapGet("/api/health/db", GetDatabaseHealthAsync);
    }

    private static IResult GetApiHealth()
    {
        return Results.Ok(new
        {
            status = "ok",
            service = "e-state-api"
        });
    }

    private static async Task<IResult> GetDatabaseHealthAsync(
        EStateDbContext db,
        CancellationToken cancellationToken)
    {
        var canConnect = await db.Database.CanConnectAsync(cancellationToken);

        return canConnect
            ? Results.Ok(new { database = "ok" })
            : Results.Problem(
                statusCode: StatusCodes.Status503ServiceUnavailable,
                title: "Database unavailable");
    }
}

using EState.Api.Modules.Listings;
using EState.Application.Modules.Listings;
using EState.Infrastructure.Persistence;
using EState.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ListingsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapListingsEndpoints();

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    service = "e-state-api"
}));

app.MapGet("/api/health/db", async (
    EStateDbContext db,
    CancellationToken cancellationToken) =>
{
    var canConnect = await db.Database.CanConnectAsync(cancellationToken);

    return canConnect
        ? Results.Ok(new { database = "ok" })
        : Results.Problem(
            statusCode: StatusCodes.Status503ServiceUnavailable,
            title: "Database unavailable"
        );
});

app.Run();

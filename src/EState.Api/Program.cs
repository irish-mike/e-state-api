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
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "e-state API";
        options.SwaggerEndpoint("../openapi/v1.json", "e-state API");
    });

    app.Lifetime.ApplicationStarted.Register(() =>
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
    });
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

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EState.IntegrationTests.Health;

public sealed class HealthEndpointTests(WebApplicationFactory<Program> factory)
	: IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client = factory
		.WithWebHostBuilder(builder => builder.UseSetting(
			"ConnectionStrings:PostgreSql",
			"Host=127.0.0.1;Port=1;Database=e_state_test;Username=e_state"))
		.CreateClient();

	[Fact]
	public async Task GetHealth_ReturnsOk()
	{
		var response = await _client.GetAsync("/api/health");

		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}
}

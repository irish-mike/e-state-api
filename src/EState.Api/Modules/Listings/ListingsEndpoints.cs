using EState.Application.Modules.Listings;
using EState.Application.Modules.Listings.CreateListing;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EState.Api.Modules.Listings;

public static class ListingsEndpoints
{
	private static readonly Guid DevelopmentSellerId = new("20000000-0000-4000-8000-000000000003");

	public static void MapListingsEndpoints(this IEndpointRouteBuilder app)
	{
		var listings = app.MapGroup("/api/listings");

		listings.MapGet("/{id:guid}", GetListing)
			.Produces<ListingResponse>()
			.Produces(StatusCodes.Status404NotFound);

		var environment = app.ServiceProvider.GetRequiredService<IHostEnvironment>();
		if (!environment.IsDevelopment())
			return;

		listings.MapPost("", CreateListing)
			.ProducesValidationProblem();
	}

	private static async Task<IResult> GetListing(Guid id, ListingsService listingService)
	{
		var listing = await listingService.GetByIdAsync(id);

		if (listing is null)
			return Results.NotFound();

		return Results.Ok(listing.ToResponse());
	}

	private static async Task<Created<CreateListingResponse>> CreateListing(
		CreateListingRequest request,
		CreateListingHandler handler,
		CancellationToken cancellationToken)
	{
		// TODO: Resolve the seller from the authenticated user before enabling this endpoint outside Development.
		var command = request.ToCommand(DevelopmentSellerId);
		var listingId = await handler.HandleAsync(command, cancellationToken);

		return TypedResults.Created($"/api/listings/{listingId}", new CreateListingResponse(listingId));
	}
}

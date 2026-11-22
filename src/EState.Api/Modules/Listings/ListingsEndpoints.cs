using EState.Application.Modules.Listings;

namespace EState.Api.Modules.Listings;

public static class ListingsEndpoints
{
	public static void MapListingsEndpoints(this IEndpointRouteBuilder app)
	{
		var listings = app.MapGroup("/api/listings");

		listings.MapGet("/{id:guid}", GetListing)
			.Produces<ListingResponse>();
	}

	private static async Task<IResult> GetListing(Guid id, ListingsService listingService)
	{
		var listing = await listingService.GetByIdAsync(id);

		return Results.Ok(listing.ToResponse());
	}
}

using EState.Api.Shared.Serialization;
using EState.Application.Modules.Listings.CreateListing;
using EState.Domain.Modules.Properties;

namespace EState.Api.Modules.Listings;

internal static class CreateListingRequestMapping
{
	public static CreateListingCommand ToCommand(this CreateListingRequest request, Guid sellerId)
	{
		var property = request.Property;
		var address = property.Address;

		var propertyType = SnakeCaseEnum<PropertyType>.Parse(property.PropertyType);
		var berRating = SnakeCaseEnum<BerRating>.ParseNullable(property.BerRating);

		var listingAddress = new CreateListingAddress(
			address.Line1,
			address.Line2,
			address.Town,
			address.County,
			address.Eircode);

		var listingProperty = new CreateListingProperty(
			propertyType,
			property.Bedrooms,
			property.Bathrooms,
			property.YearBuilt,
			berRating,
			property.FloorAreaSqm,
			property.Description,
			listingAddress);

		return new CreateListingCommand(sellerId, request.AskingPrice, listingProperty);
	}
}

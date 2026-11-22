using EState.Domain.Modules.Properties;

namespace EState.Application.Modules.Listings.CreateListing;

public sealed record CreateListingCommand(
	Guid SellerId,
	int? AskingPrice,
	CreateListingProperty Property);

public sealed record CreateListingProperty(
	PropertyType PropertyType,
	short Bedrooms,
	short Bathrooms,
	short? YearBuilt,
	BerRating? BerRating,
	int? FloorAreaSqm,
	string? Description,
	CreateListingAddress Address);

public sealed record CreateListingAddress(
	string Line1,
	string? Line2,
	string Town,
	string County,
	string Eircode);

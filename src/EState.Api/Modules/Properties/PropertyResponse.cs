using EState.Api.Modules.Addresses;
using EState.Domain.Modules.Properties;

namespace EState.Api.Modules.Properties;

public sealed record PropertyResponse(
	Guid Id,
	AddressResponse Address,
	PropertyType PropertyType,
	short Bedrooms,
	short Bathrooms,
	short? YearBuilt,
	BerRating? BerRating,
	int? FloorAreaSqm,
	string? Description);

using EState.Api.Modules.Addresses;

namespace EState.Api.Modules.Properties;

public sealed record PropertyResponse(
    Guid Id,
    AddressResponse Address,
    string PropertyType,
    short Bedrooms,
    short Bathrooms,
    short? YearBuilt,
    string? BerRating,
    decimal? FloorAreaSqm,
    string? Description);

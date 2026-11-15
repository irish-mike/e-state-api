using System.Text.Json;
using EState.Api.Modules.Addresses;
using EState.Api.Modules.Properties;
using EState.Domain.Modules.Listings;

namespace EState.Api.Modules.Listings;

public sealed record ListingResponse(
    Guid Id,
    Guid SellerId,
    decimal? AskingPrice,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    PropertyResponse Property);

internal static class ListingResponseMapping
{
    public static ListingResponse ToResponse(this Listing listing)
    {
        var property = listing.Property;
        var address = property.Address;

        var addressResponse = new AddressResponse(
            address.Id,
            address.Line1,
            address.Line2,
            address.Town,
            address.County,
            address.Eircode,
            address.CountryCode);

        var propertyResponse = new PropertyResponse(
            property.Id,
            addressResponse,
            JsonNamingPolicy.SnakeCaseLower.ConvertName(property.PropertyType.ToString()),
            property.Bedrooms,
            property.Bathrooms,
            property.YearBuilt,
            property.BerRating?.ToString(),
            property.FloorAreaSqm,
            property.Description);

        return new ListingResponse(
            listing.Id,
            listing.SellerId,
            listing.AskingPrice,
            JsonNamingPolicy.SnakeCaseLower.ConvertName(listing.Status.ToString()),
            listing.CreatedAt,
            listing.UpdatedAt,
            propertyResponse);
    }
}

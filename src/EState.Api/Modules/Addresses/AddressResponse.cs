namespace EState.Api.Modules.Addresses;

public sealed record AddressResponse(
    Guid Id,
    string Line1,
    string? Line2,
    string Town,
    string County,
    string Eircode,
    string CountryCode);

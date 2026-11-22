using System.ComponentModel.DataAnnotations;
using EState.Api.Shared.Validation;
using EState.Domain.Modules.Properties;

namespace EState.Api.Modules.Listings;

public sealed record CreateListingRequest
{
	[Range(typeof(decimal), "0", "9999999999.99", ParseLimitsInInvariantCulture = true)]
	public decimal? AskingPrice { get; init; }

	public required PropertyDetails Property { get; init; }
}

public sealed record PropertyDetails
{
	[EnumValue<PropertyType>(ErrorMessage = "Choose a supported property type.")]
	public required string PropertyType { get; init; }

	[Range(0, 100)]
	public required short Bedrooms { get; init; }

	[Range(0, 100)]
	public required short Bathrooms { get; init; }

	[Range(1, 2100)]
	public short? YearBuilt { get; init; }

	[EnumValue<BerRating>(ErrorMessage = "Choose a supported BER rating.")]
	public string? BerRating { get; init; }

	[Range(typeof(decimal), "0", "99999999.99", ParseLimitsInInvariantCulture = true)]
	public decimal? FloorAreaSqm { get; init; }

	public string? Description { get; init; }
	public required AddressDetail Address { get; init; }
}

public sealed record AddressDetail
{
	[Required(ErrorMessage = "Address line 1 is required.")]
	[StringLength(255)]
	public required string Line1 { get; init; }

	[StringLength(255)]
	public string? Line2 { get; init; }

	[Required(ErrorMessage = "Town is required.")]
	[StringLength(100)]
	public required string Town { get; init; }

	[Required(ErrorMessage = "County is required.")]
	[StringLength(100)]
	public required string County { get; init; }

	[Required(ErrorMessage = "Eircode is required.")]
	[StringLength(8)]
	public required string Eircode { get; init; }
}

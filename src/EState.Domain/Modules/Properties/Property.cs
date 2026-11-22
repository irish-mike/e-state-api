using EState.Domain.Common;
using EState.Domain.Modules.Addresses;

namespace EState.Domain.Modules.Properties;

public sealed class Property : ITimestampedEntity
{
	public Guid Id { get; set; }
	public Guid AddressId { get; set; }
	public Address Address { get; set; } = null!;
	public required PropertyType PropertyType { get; set; }
	public short Bedrooms { get; set; }
	public short Bathrooms { get; set; }
	public short? YearBuilt { get; set; }
	public BerRating? BerRating { get; set; }
	public decimal? FloorAreaSqm { get; set; }
	public string? Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
}

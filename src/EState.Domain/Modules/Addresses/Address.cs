using EState.Domain.Common;

namespace EState.Domain.Modules.Addresses;

public sealed class Address : ITimestampedEntity
{
	public Guid Id { get; set; }
	public required string Line1 { get; set; }
	public string? Line2 { get; set; }
	public required string Town { get; set; }
	public required string County { get; set; }
	public required string Eircode { get; set; }
	public string CountryCode { get; set; } = "IE";
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
}

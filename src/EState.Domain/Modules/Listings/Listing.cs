using EState.Domain.Common;
using EState.Domain.Modules.Properties;
using EState.Domain.Modules.Users;

namespace EState.Domain.Modules.Listings;

public sealed class Listing : ITimestampedEntity
{
	public Guid Id { get; set; }
	public Guid PropertyId { get; set; }
	public Property Property { get; set; } = null!;
	public Guid SellerId { get; set; }
	public User Seller { get; set; } = null!;
	public int? AskingPrice { get; set; }
	public ListingStatus Status { get; set; } = ListingStatus.Draft;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
}

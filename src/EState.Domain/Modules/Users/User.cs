using EState.Domain.Common;
using EState.Domain.Modules.Addresses;

namespace EState.Domain.Modules.Users;

public sealed class User : ITimestampedEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
    public Guid? SolicitorId { get; set; }
    public string? ProfileImageKey { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

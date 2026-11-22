namespace EState.Domain.Common;

public interface ITimestampedEntity
{
	DateTime CreatedAt { get; set; }
	DateTime UpdatedAt { get; set; }
	DateTime? DeletedAt { get; set; }
}

using EState.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EState.Infrastructure.Persistence.Configurations;

internal static class TimestampConfiguration
{
    public static void ConfigureTimestamps<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ITimestampedEntity
    {
        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(entity => entity.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp with time zone");
    }
}

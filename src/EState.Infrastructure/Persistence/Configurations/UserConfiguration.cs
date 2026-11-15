using EState.Domain.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EState.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureTable(builder);

        ConfigureColumns(builder);

        ConfigureIndexes(builder);

        ConfigureRelationships(builder);
    }

    private static void ConfigureTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id)
            .HasName("pk_users");
    }

    private static void ConfigureColumns(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Id)
            .HasColumnName("id");

        builder.Property(user => user.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(user => user.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(user => user.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(30);

        builder.Property(user => user.AddressId)
            .HasColumnName("address_id");

        builder.Property(user => user.SolicitorId)
            .HasColumnName("solicitor_id");

        builder.Property(user => user.ProfileImageKey)
            .HasColumnName("profile_image_key")
            .HasMaxLength(500);

        builder.Property(user => user.IsVerified)
            .HasColumnName("is_verified")
            .HasDefaultValue(false);

        builder.ConfigureTimestamps();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(user => user.Email)
            .IsUnique()
            .HasDatabaseName("ux_users_email");

        builder.HasIndex(user => user.AddressId)
            .HasDatabaseName("ix_users_address_id");
    }

    private static void ConfigureRelationships(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(user => user.Address)
            .WithMany()
            .HasForeignKey(user => user.AddressId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_users_addresses_address_id");
    }
}

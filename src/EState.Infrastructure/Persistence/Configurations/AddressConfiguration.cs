using EState.Domain.Modules.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EState.Infrastructure.Persistence.Configurations;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        ConfigureTable(builder);

        ConfigureColumns(builder);
    }

    private static void ConfigureTable(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(address => address.Id)
            .HasName("pk_addresses");
    }

    private static void ConfigureColumns(EntityTypeBuilder<Address> builder)
    {
        builder.Property(address => address.Id)
            .HasColumnName("id");

        builder.Property(address => address.Line1)
            .HasColumnName("line_1")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(address => address.Line2)
            .HasColumnName("line_2")
            .HasMaxLength(255);

        builder.Property(address => address.Town)
            .HasColumnName("town")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(address => address.County)
            .HasColumnName("county")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(address => address.Eircode)
            .HasColumnName("eircode")
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(address => address.CountryCode)
            .HasColumnName("country_code")
            .HasColumnType("character(2)")
            .HasDefaultValue("IE")
            .IsRequired();

        builder.ConfigureTimestamps();
    }
}

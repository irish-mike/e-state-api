using System.Text.Json;
using EState.Domain.Modules.Listings;
using EState.Infrastructure.Persistence.Utils;
using EState.Infrastructure.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EState.Infrastructure.Persistence.Configurations;

public sealed class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        ConfigureTable(builder);

        ConfigureColumns(builder);

        ConfigureIndexes(builder);

        ConfigureRelationships(builder);
    }

    private static void ConfigureTable(EntityTypeBuilder<Listing> builder)
    {
        builder.ToTable("listings", table =>
        {
            table.HasCheckConstraint("ck_listings_status",
                DbUtils.AllowedValues("status",
                    SharedUtils.SnakeCaseEnumNames<ListingStatus>()));

            table.HasCheckConstraint("ck_listings_asking_price_non_negative", "asking_price >= 0");
        });

        builder.HasKey(listing => listing.Id)
            .HasName("pk_listings");
    }

    private static void ConfigureColumns(EntityTypeBuilder<Listing> builder)
    {
        builder.Property(listing => listing.Id)
            .HasColumnName("id");

        builder.Property(listing => listing.PropertyId)
            .HasColumnName("property_id");

        builder.Property(listing => listing.SellerId)
            .HasColumnName("seller_id");

        builder.Property(listing => listing.AskingPrice)
            .HasColumnName("asking_price")
            .HasPrecision(12, 2);

        builder.Property(listing => listing.Status)
            .HasColumnName("status")
            .HasConversion(
                value => JsonNamingPolicy.SnakeCaseLower.ConvertName(value.ToString()),
                value => Enum.Parse<ListingStatus>(value, true))
            .HasMaxLength(20)
            .HasDefaultValue(ListingStatus.Draft)
            .IsRequired();

        builder.ConfigureTimestamps();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Listing> builder)
    {
        builder.HasIndex(listing => listing.PropertyId)
            .HasDatabaseName("ix_listings_property_id");

        builder.HasIndex(listing => listing.SellerId)
            .HasDatabaseName("ix_listings_seller_id");
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Listing> builder)
    {
        builder.HasOne(listing => listing.Property)
            .WithMany()
            .HasForeignKey(listing => listing.PropertyId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_listings_properties_property_id");

        builder.HasOne(listing => listing.Seller)
            .WithMany()
            .HasForeignKey(listing => listing.SellerId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_listings_users_seller_id");
    }
}

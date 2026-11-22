using System.Text.Json;
using EState.Domain.Modules.Properties;
using EState.Infrastructure.Persistence.Utils;
using EState.Infrastructure.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EState.Infrastructure.Persistence.Configurations;

public sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
	public void Configure(EntityTypeBuilder<Property> builder)
	{
		ConfigureTable(builder);

		ConfigureColumns(builder);

		ConfigureIndexes(builder);

		ConfigureRelationships(builder);
	}

	private static void ConfigureTable(EntityTypeBuilder<Property> builder)
	{
		builder.ToTable("properties", table =>
		{
			table.HasCheckConstraint("ck_properties_property_type",
				DbUtils.AllowedValues("property_type",
					SharedUtils.SnakeCaseEnumNames<PropertyType>()));

			table.HasCheckConstraint("ck_properties_ber_rating",
				DbUtils.AllowedValues("ber_rating", Enum.GetNames<BerRating>()));

			table.HasCheckConstraint("ck_properties_bedrooms_non_negative", "bedrooms >= 0");

			table.HasCheckConstraint("ck_properties_bathrooms_non_negative", "bathrooms >= 0");

			table.HasCheckConstraint("ck_properties_year_built_positive", "year_built > 0");

			table.HasCheckConstraint("ck_properties_floor_area_sqm_non_negative", "floor_area_sqm >= 0");
		});

		builder.HasKey(property => property.Id)
			.HasName("pk_properties");
	}

	private static void ConfigureColumns(EntityTypeBuilder<Property> builder)
	{
		builder.Property(property => property.Id)
			.HasColumnName("id");

		builder.Property(property => property.AddressId)
			.HasColumnName("address_id");

		builder.Property(property => property.PropertyType)
			.HasColumnName("property_type")
			.HasConversion(
				value => JsonNamingPolicy.SnakeCaseLower.ConvertName(value.ToString()),
				value => Enum.Parse<PropertyType>(value.Replace("_", string.Empty), true))
			.HasMaxLength(30)
			.IsRequired();

		builder.Property(property => property.Bedrooms)
			.HasColumnName("bedrooms");

		builder.Property(property => property.Bathrooms)
			.HasColumnName("bathrooms");

		builder.Property(property => property.YearBuilt)
			.HasColumnName("year_built");

		builder.Property(property => property.BerRating)
			.HasColumnName("ber_rating")
			.HasConversion<string>()
			.HasMaxLength(6);

		builder.Property(property => property.FloorAreaSqm)
			.HasColumnName("floor_area_sqm");

		builder.Property(property => property.Description)
			.HasColumnName("description")
			.HasColumnType("text");

		builder.ConfigureTimestamps();
	}

	private static void ConfigureIndexes(EntityTypeBuilder<Property> builder)
	{
		builder.HasIndex(property => property.AddressId)
			.IsUnique()
			.HasDatabaseName("ux_properties_address_id");
	}

	private static void ConfigureRelationships(EntityTypeBuilder<Property> builder)
	{
		builder.HasOne(property => property.Address)
			.WithOne()
			.HasForeignKey<Property>(property => property.AddressId)
			.OnDelete(DeleteBehavior.Restrict)
			.HasConstraintName("fk_properties_addresses_address_id");
	}
}

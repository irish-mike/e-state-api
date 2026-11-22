using EState.Application.Abstractions.Persistence;
using EState.Domain.Modules.Addresses;
using EState.Domain.Modules.Listings;
using EState.Domain.Modules.Properties;

namespace EState.Application.Modules.Listings.CreateListing;

public sealed class CreateListingHandler(IListingRepository listingRepository, IUnitOfWork unitOfWork)
{
	public async Task<Guid> HandleAsync(CreateListingCommand command, CancellationToken cancellationToken)
	{
		var propertyDetails = command.Property;
		var addressDetails = propertyDetails.Address;
		var address = new Address
		{
			Line1 = addressDetails.Line1,
			Line2 = addressDetails.Line2,
			Town = addressDetails.Town,
			County = addressDetails.County,
			Eircode = addressDetails.Eircode
		};

		var property = new Property
		{
			Address = address,
			PropertyType = propertyDetails.PropertyType,
			Bedrooms = propertyDetails.Bedrooms,
			Bathrooms = propertyDetails.Bathrooms,
			YearBuilt = propertyDetails.YearBuilt,
			BerRating = propertyDetails.BerRating,
			FloorAreaSqm = propertyDetails.FloorAreaSqm,
			Description = propertyDetails.Description
		};

		var listing = new Listing
		{
			SellerId = command.SellerId,
			AskingPrice = command.AskingPrice,
			Property = property
		};

		listingRepository.Add(listing);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return listing.Id;
	}
}

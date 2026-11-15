using EState.Domain.Modules.Listings;

namespace EState.Application.Modules.Listings;

public sealed class ListingsService(IListingRepository listingRepository)
{
    public Task<Listing> GetByIdAsync(Guid id)
    {
        return listingRepository.GetByIdAsync(id);
    }
}

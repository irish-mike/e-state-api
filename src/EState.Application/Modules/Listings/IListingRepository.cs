using EState.Domain.Modules.Listings;

namespace EState.Application.Modules.Listings;

public interface IListingRepository
{
	Task<Listing?> GetByIdAsync(Guid id);
	void Add(Listing listing);
}

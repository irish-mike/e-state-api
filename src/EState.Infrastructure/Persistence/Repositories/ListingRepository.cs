using EState.Application.Modules.Listings;
using EState.Domain.Modules.Listings;
using Microsoft.EntityFrameworkCore;

namespace EState.Infrastructure.Persistence.Repositories;

public sealed class ListingRepository(EStateDbContext db) : IListingRepository
{
	public Task<Listing?> GetByIdAsync(Guid id)
	{
		return db.Listings
			.AsNoTracking()
			.Include(listing => listing.Property)
			.ThenInclude(property => property.Address)
			.SingleOrDefaultAsync(listing => listing.Id == id);
	}
}

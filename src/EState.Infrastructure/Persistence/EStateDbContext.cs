using EState.Domain.Modules.Addresses;
using EState.Domain.Modules.Listings;
using EState.Domain.Modules.Properties;
using EState.Domain.Modules.Users;
using Microsoft.EntityFrameworkCore;

namespace EState.Infrastructure.Persistence;

public sealed class EStateDbContext(
    DbContextOptions<EStateDbContext> options)
    : DbContext(options)
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Listing> Listings => Set<Listing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EStateDbContext).Assembly);
    }
}

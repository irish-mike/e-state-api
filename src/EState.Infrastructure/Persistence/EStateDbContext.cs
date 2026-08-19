using Microsoft.EntityFrameworkCore;

namespace EState.Infrastructure.Persistence;

public sealed class EStateDbContext(
    DbContextOptions<EStateDbContext> options)
    : DbContext(options) { }
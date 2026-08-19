using EState.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EState.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("PostgreSql") ?? 
            throw new InvalidOperationException("PostgreSQL connection string is not configured." );

        services.AddDbContext<EStateDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}
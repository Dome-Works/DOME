using HomelabDocs.Domain.Seeding;
using HomelabDocs.Domain.Sockets;
using HomelabDocs.Domain.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabDocs.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddHomelabDocsDomain(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = SqlitePath.ResolveConnectionString(configuration);
        SqlitePath.EnsureDataDirectory(connectionString);

        services.AddDbContext<HomelabDocsDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<ISocketRepository, SocketRepository>();

        return services;
    }
}

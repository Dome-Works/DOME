using Dome.Domain.Images;
using Dome.Domain.Seeding;
using Dome.Domain.Settings;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Domain.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomeDomain(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = SqlitePath.ResolveConnectionString(configuration);
        SqlitePath.EnsureDataDirectory(connectionString);

        services.AddDbContext<DomeDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<ISocketRepository, SocketRepository>();
        services.AddScoped<IStackRepository, StackRepository>();
        services.AddScoped<IImageVersionCacheRepository, ImageVersionCacheRepository>();
        services.AddScoped<ISystemSettingRepository, SystemSettingRepository>();

        return services;
    }
}

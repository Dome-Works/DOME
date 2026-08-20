using HomelabDocs.Business.Devices;
using HomelabDocs.Business.Sockets;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabDocs.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddHomelabDocsBusiness(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(HomelabDocsSocketApiFactory));
        services.AddSingleton<IHomelabDocsSocketApiFactory, HomelabDocsSocketApiFactory>();
        services.AddScoped<ISocketService, SocketService>();
        services.AddScoped<IDeviceQueryService, DeviceQueryService>();

        return services;
    }
}

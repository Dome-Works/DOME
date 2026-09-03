using Dome.Business.Devices;
using Dome.Business.Sockets;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddDomeBusiness(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(DomeSocketApiFactory));
        services.AddSingleton<IDomeSocketApiFactory, DomeSocketApiFactory>();
        services.AddScoped<ISocketService, SocketService>();
        services.AddScoped<IDeviceQueryService, DeviceQueryService>();
        services.AddScoped<IDeviceContainerCommandService, DeviceContainerCommandService>();

        return services;
    }
}

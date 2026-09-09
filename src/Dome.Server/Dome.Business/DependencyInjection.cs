using Dome.Business.Devices;
using Dome.Business.Sockets;
using Dome.Business.Stacks;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddDomeBusiness(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(DomeSocketApiFactory), client =>
        {
            client.Timeout = TimeSpan.FromMinutes(15);
        });
        services.AddSingleton<IDomeSocketApiFactory, DomeSocketApiFactory>();
        services.AddScoped<ISocketService, SocketService>();
        services.AddScoped<IDeviceQueryService, DeviceQueryService>();
        services.AddScoped<IDeviceDiagramService, DeviceDiagramService>();
        services.AddScoped<IDeviceContainerCommandService, DeviceContainerCommandService>();
        services.AddScoped<IDeviceStackDeployService, DeviceStackDeployService>();
        services.AddScoped<IStackService, StackService>();

        return services;
    }
}

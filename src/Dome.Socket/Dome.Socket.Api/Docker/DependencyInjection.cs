using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Api.Docker.Compose;
using Dome.Socket.Api.Docker.Configuration;
using Dome.Socket.Api.Docker.Images;
using Dome.Socket.Api.Docker.Processes;
using Dome.Socket.Api.Docker.Registries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Socket.Api.Docker;

public static class DependencyInjection
{
    public static IServiceCollection AddDomeSocket(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DockerEndpointOptions>(
            configuration.GetSection(DockerEndpointOptions.SectionName));
        services.Configure<DockerComposeOptions>(
            configuration.GetSection(DockerComposeOptions.SectionName));

        services.AddSingleton<IProcessRunner, ProcessRunner>();
        services.AddSingleton<IDockerEngineClient, DockerEngineClient>();
        services.AddSingleton<IDockerComposeClient, DockerComposeClient>();
        services.AddHttpClient<DockerHubRegistryProvider>(ConfigureRegistryHttpClient);
        services.AddHttpClient<GhcrRegistryProvider>(ConfigureRegistryHttpClient);
        services.AddTransient<IRegistryProvider>(static provider =>
            provider.GetRequiredService<DockerHubRegistryProvider>());
        services.AddTransient<IRegistryProvider>(static provider =>
            provider.GetRequiredService<GhcrRegistryProvider>());
        services.AddTransient<IImageVersionCheckService, ImageVersionCheckService>();

        return services;
    }

    private static void ConfigureRegistryHttpClient(HttpClient client)
    {
        client.Timeout = TimeSpan.FromSeconds(20);
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Dome.Socket");
    }
}

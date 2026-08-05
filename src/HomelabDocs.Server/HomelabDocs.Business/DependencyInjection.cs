using Docker.DotNet;
using HomelabDocs.Business.Docker.Clients;
using HomelabDocs.Business.Docker.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabDocs.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddHomelabDocsBusiness(
        this IServiceCollection services)
    {
        services.AddSingleton<IDockerClient>(_ =>
            new DockerClientConfiguration(
                    new Uri(DockerConnectionOptions.DefaultDockerSocket))
                .CreateClient());

        services.AddSingleton<IDockerContainerClient, DockerContainerClient>();

        return services;
    }
}

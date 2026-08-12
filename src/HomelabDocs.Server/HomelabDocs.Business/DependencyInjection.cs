using HomelabDocs.Business.Docker.Clients;
using HomelabDocs.Business.Docker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabDocs.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddHomelabDocsBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DockerConnectionOptions>(
            configuration.GetSection(DockerConnectionOptions.SectionName));

        services.AddSingleton<IDockerClientRegistry, DockerClientRegistry>();
        services.AddSingleton<IDockerContainerClient, DockerContainerClient>();

        return services;
    }
}

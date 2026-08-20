using HomelabDocs.Socket.Api.Docker.Clients;
using HomelabDocs.Socket.Api.Docker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabDocs.Socket.Api.Docker;

public static class DependencyInjection
{
    public static IServiceCollection AddHomelabDocsSocket(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DockerEndpointOptions>(
            configuration.GetSection(DockerEndpointOptions.SectionName));

        services.AddSingleton<IDockerEngineClient, DockerEngineClient>();

        return services;
    }
}

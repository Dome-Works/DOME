using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Api.Docker.Configuration;
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

        services.AddSingleton<IDockerEngineClient, DockerEngineClient>();

        return services;
    }
}

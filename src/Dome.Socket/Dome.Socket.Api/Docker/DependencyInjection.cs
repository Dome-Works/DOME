using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Api.Docker.Compose;
using Dome.Socket.Api.Docker.Configuration;
using Dome.Socket.Api.Docker.Processes;
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

        return services;
    }
}

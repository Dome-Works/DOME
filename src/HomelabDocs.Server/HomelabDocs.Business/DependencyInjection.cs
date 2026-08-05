using Docker.DotNet;
using HomelabDocs.Business.Docker.Clients;
using HomelabDocs.Business.Docker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HomelabDocs.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddHomelabDocsBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DockerConnectionOptions>(
            configuration.GetSection(DockerConnectionOptions.SectionName));

        services.AddSingleton<IDockerClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<DockerConnectionOptions>>().Value;
            using var configuration = new DockerClientConfiguration(new Uri(options.Endpoint));
            return configuration.CreateClient();
        });

        services.AddSingleton<IDockerContainerClient, DockerContainerClient>();

        return services;
    }
}

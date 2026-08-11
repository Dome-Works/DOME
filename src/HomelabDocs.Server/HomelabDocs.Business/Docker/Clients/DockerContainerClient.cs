using Docker.DotNet;
using Docker.DotNet.Models;
using HomelabDocs.Shared.Containers;
using Microsoft.Extensions.Logging;

namespace HomelabDocs.Business.Docker.Clients;

public sealed class DockerContainerClient : IDockerContainerClient
{
    private readonly IDockerClient _dockerClient;
    private readonly ILogger<DockerContainerClient> _logger;

    public DockerContainerClient(
        IDockerClient dockerClient,
        ILogger<DockerContainerClient> logger)
    {
        _dockerClient = dockerClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<ContainerResponse>> GetRunningContainersAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var containers = await _dockerClient.Containers.ListContainersAsync(
                new ContainersListParameters
                {
                    All = true
                },
                cancellationToken);

            if (containers is null || containers.Count == 0)
            {
                return Array.Empty<ContainerResponse>();
            }

            return containers
                .Select(ContainerMapper.Map)
                .ToArray();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Failed to list Docker containers from the configured Docker Engine.");
            throw;
        }
    }
}

using Docker.DotNet.Models;
using HomelabDocs.Shared.Containers;
using Microsoft.Extensions.Logging;

namespace HomelabDocs.Business.Docker.Clients;

public sealed class DockerContainerClient : IDockerContainerClient
{
    private readonly IDockerClientRegistry _dockerClientRegistry;
    private readonly ILogger<DockerContainerClient> _logger;

    public DockerContainerClient(
        IDockerClientRegistry dockerClientRegistry,
        ILogger<DockerContainerClient> logger)
    {
        _dockerClientRegistry = dockerClientRegistry;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<ContainerResponse>> GetRunningContainersAsync(
        string deviceName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceName);

        if (!_dockerClientRegistry.TryGetClient(deviceName, out var dockerClient))
        {
            throw new KeyNotFoundException($"Docker device '{deviceName}' was not found.");
        }

        try
        {
            var containers = await dockerClient.Containers.ListContainersAsync(
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
                "Failed to list Docker containers from device '{DeviceName}'.",
                deviceName);
            throw;
        }
    }
}

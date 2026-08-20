using Docker.DotNet;
using Docker.DotNet.Models;
using HomelabDocs.Socket.Api.Docker.Configuration;
using HomelabDocs.Socket.Contracts.Containers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HomelabDocs.Socket.Api.Docker.Clients;

internal sealed class DockerEngineClient : IDockerEngineClient, IDisposable
{
    private readonly IDockerClient _dockerClient;
    private readonly ILogger<DockerEngineClient> _logger;
    private bool _disposed;

    public DockerEngineClient(
        IOptions<DockerEndpointOptions> options,
        ILogger<DockerEngineClient> logger)
    {
        ArgumentNullException.ThrowIfNull(options);
        _logger = logger;

        var endpoint = string.IsNullOrWhiteSpace(options.Value.Endpoint)
            ? DockerEndpointOptions.DefaultEndpoint
            : options.Value.Endpoint;

        var uri = DockerEndpointValidator.Validate(endpoint);
        using var configuration = new DockerClientConfiguration(uri);
        _dockerClient = configuration.CreateClient();
    }

    public async Task<bool> IsReachableAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _dockerClient.System.PingAsync(cancellationToken);
            return true;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(ex, "Docker Engine ping failed.");
            return false;
        }
    }

    public async Task<IReadOnlyCollection<ContainerResponse>> ListContainersAsync(
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
            _logger.LogError(ex, "Failed to list Docker containers.");
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _dockerClient.Dispose();
        _disposed = true;
    }
}

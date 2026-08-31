using Docker.DotNet;
using Docker.DotNet.Models;
using HomelabDocs.Socket.Api.Docker.Configuration;
using HomelabDocs.Socket.Contracts.Containers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;

namespace HomelabDocs.Socket.Api.Docker.Clients;

internal sealed class DockerEngineClient : IDockerEngineClient, IDisposable
{
    private readonly IDockerClient _dockerClient;
    private readonly HttpClient _httpClient;
    private readonly ILogger<DockerEngineClient> _logger;
    private bool _disposed;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

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
        _httpClient = CreateHttpClient(uri);
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

            var mappedContainers = containers
                .Select(ContainerMapper.Map)
                .ToArray();

            return await EnrichVolumeSizesAsync(mappedContainers, cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Failed to list Docker containers.");
            throw;
        }
    }

    private async Task<IReadOnlyCollection<ContainerResponse>> EnrichVolumeSizesAsync(
        IReadOnlyCollection<ContainerResponse> containers,
        CancellationToken cancellationToken)
    {
        var volumeNames = containers
            .SelectMany(static container => container.Volumes)
            .Where(static volume =>
                string.Equals(volume.Type, "volume", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(volume.Name))
            .Select(static volume => volume.Name!)
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (volumeNames.Length == 0)
        {
            return containers;
        }

        var sizeByVolumeName = await LoadVolumeSizesAsync(cancellationToken);

        return containers
            .Select(container =>
                container with
                {
                    Volumes = container.Volumes
                        .Select(volume =>
                        {
                            if (volume.Name is null)
                            {
                                return volume;
                            }

                            if (!sizeByVolumeName.TryGetValue(volume.Name, out var sizeBytes))
                            {
                                return volume;
                            }

                            return volume with
                            {
                                SizeBytes = sizeBytes,
                            };
                        })
                        .ToArray(),
                })
            .ToArray();
    }

    private async Task<IReadOnlyDictionary<string, long?>> LoadVolumeSizesAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/system/df?type=volume");

            using var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogDebug(
                    "Docker system df returned status code {StatusCode}.",
                    response.StatusCode);
                return new Dictionary<string, long?>(StringComparer.Ordinal);
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var payload = await JsonSerializer.DeserializeAsync<SystemDataUsageResponse>(
                stream,
                JsonOptions,
                cancellationToken);

            if (payload?.Volumes is null || payload.Volumes.Length == 0)
            {
                return new Dictionary<string, long?>(StringComparer.Ordinal);
            }

            return payload.Volumes
                .Where(static volume => !string.IsNullOrWhiteSpace(volume.Name))
                .ToDictionary(
                    static volume => volume.Name!,
                    static volume =>
                    {
                        var size = volume.UsageData?.Size;
                        return size is >= 0 ? size : null;
                    },
                    StringComparer.Ordinal);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogDebug(
                ex,
                "Failed to load Docker volume usage data.");
            return new Dictionary<string, long?>(StringComparer.Ordinal);
        }
    }

    private static HttpClient CreateHttpClient(Uri dockerEndpoint)
    {
        var handler = new SocketsHttpHandler
        {
            ConnectCallback = async (context, cancellationToken) =>
            {
                var socket = new System.Net.Sockets.Socket(
                    AddressFamily.Unix,
                    SocketType.Stream,
                    ProtocolType.Unspecified);
                await socket.ConnectAsync(
                    new UnixDomainSocketEndPoint(dockerEndpoint.LocalPath),
                    cancellationToken);
                return new NetworkStream(socket, ownsSocket: true);
            },
        };

        return new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost"),
        };
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _dockerClient.Dispose();
        _httpClient.Dispose();
        _disposed = true;
    }
}

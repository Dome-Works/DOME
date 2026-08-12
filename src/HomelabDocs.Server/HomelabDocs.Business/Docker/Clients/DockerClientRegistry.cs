using Docker.DotNet;
using HomelabDocs.Business.Docker.Configuration;
using HomelabDocs.Shared.Devices;
using Microsoft.Extensions.Options;

namespace HomelabDocs.Business.Docker.Clients;

public sealed class DockerClientRegistry : IDockerClientRegistry, IDisposable
{
    private readonly Dictionary<string, IDockerClient> _clients;
    private readonly IReadOnlyList<DeviceResponse> _devices;
    private bool _disposed;

    public DockerClientRegistry(IOptions<DockerConnectionOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var connections = options.Value.Connections ?? Array.Empty<DockerConnectionEntry>();
        if (connections.Count == 0)
        {
            throw new InvalidOperationException(
                "Docker:Connections must contain at least one connection.");
        }

        var clients = new Dictionary<string, IDockerClient>(StringComparer.OrdinalIgnoreCase);
        var devices = new List<DeviceResponse>(connections.Count);

        foreach (var connection in connections)
        {
            ValidateConnection(connection);

            if (clients.ContainsKey(connection.Name))
            {
                throw new InvalidOperationException(
                    $"Duplicate Docker connection name '{connection.Name}'.");
            }

            using var configuration = new DockerClientConfiguration(new Uri(connection.Endpoint));
            clients[connection.Name] = configuration.CreateClient();
            devices.Add(new DeviceResponse
            {
                Name = connection.Name
            });
        }

        _clients = clients;
        _devices = devices;
    }

    public IReadOnlyList<DeviceResponse> GetDevices() => _devices;

    public bool TryGetClient(string deviceName, out IDockerClient client)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceName);
        return _clients.TryGetValue(deviceName, out client!);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        foreach (var client in _clients.Values)
        {
            client.Dispose();
        }

        _clients.Clear();
        _disposed = true;
    }

    private static void ValidateConnection(DockerConnectionEntry connection)
    {
        if (string.IsNullOrWhiteSpace(connection.Name))
        {
            throw new InvalidOperationException(
                "Docker connection Name is required.");
        }

        if (string.IsNullOrWhiteSpace(connection.Endpoint))
        {
            throw new InvalidOperationException(
                $"Docker connection '{connection.Name}' requires an Endpoint.");
        }

        if (!Uri.TryCreate(connection.Endpoint, UriKind.Absolute, out _))
        {
            throw new InvalidOperationException(
                $"Docker connection '{connection.Name}' has an invalid Endpoint '{connection.Endpoint}'.");
        }
    }
}

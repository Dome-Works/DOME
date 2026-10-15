namespace Dome.Socket.Api.Docker.Registries;

internal sealed record RegistryVersionInfo
{
    public string? RemoteDigest { get; init; }

    public IReadOnlyCollection<string> Tags { get; init; } = Array.Empty<string>();
}

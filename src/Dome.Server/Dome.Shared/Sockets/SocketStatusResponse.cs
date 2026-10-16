namespace Dome.Shared.Sockets;

public sealed record SocketStatusResponse
{
    public required Guid Id { get; init; }

    public required bool IsReachable { get; init; }

    public string? Version { get; init; }

    public required bool NeedsUpdate { get; init; }
}

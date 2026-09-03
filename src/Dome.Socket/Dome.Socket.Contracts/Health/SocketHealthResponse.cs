namespace Dome.Socket.Contracts.Health;

public sealed record SocketHealthResponse
{
    public required bool DockerReachable { get; init; }
}

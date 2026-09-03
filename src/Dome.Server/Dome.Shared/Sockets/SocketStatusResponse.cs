namespace HomelabDocs.Shared.Sockets;

public sealed record SocketStatusResponse
{
    public required Guid Id { get; init; }

    public required bool IsReachable { get; init; }
}

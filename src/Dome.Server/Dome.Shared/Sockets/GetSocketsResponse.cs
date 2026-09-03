namespace HomelabDocs.Shared.Sockets;

public sealed record GetSocketsResponse
{
    public IReadOnlyCollection<SocketResponse> Sockets { get; init; }
        = Array.Empty<SocketResponse>();
}

namespace HomelabDocs.Shared.Sockets;

public sealed record SocketResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Address { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}

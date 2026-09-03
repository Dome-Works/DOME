namespace HomelabDocs.Shared.Sockets;

public sealed record GetSocketRequest
{
    public Guid Id { get; init; }
}

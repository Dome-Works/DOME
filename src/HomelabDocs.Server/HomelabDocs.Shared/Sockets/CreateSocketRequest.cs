namespace HomelabDocs.Shared.Sockets;

public sealed record CreateSocketRequest
{
    public string Name { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;
}

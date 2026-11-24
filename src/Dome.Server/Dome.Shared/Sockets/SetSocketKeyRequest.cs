namespace Dome.Shared.Sockets;

public sealed record SetSocketKeyRequest
{
    public Guid Id { get; init; }

    public string Key { get; init; } = string.Empty;
}

namespace Dome.Shared.Sockets;

public sealed record UpdateSocketRequest
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;
}

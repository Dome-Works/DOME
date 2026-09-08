using Dome.Domain.Sockets;

namespace Dome.Domain.Stacks;

public sealed class Stack
{
    public Guid Id { get; set; }

    public Guid SocketId { get; set; }

    public Socket Socket { get; set; } = null!;

    public string ComposeName { get; set; } = string.Empty;

    public string ProjectName { get; set; } = string.Empty;

    public string ComposeYaml { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}

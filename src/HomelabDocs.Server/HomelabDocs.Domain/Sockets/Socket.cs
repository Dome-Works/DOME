namespace HomelabDocs.Domain.Sockets;

public sealed class Socket
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
}

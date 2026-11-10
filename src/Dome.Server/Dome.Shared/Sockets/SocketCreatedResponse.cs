namespace Dome.Shared.Sockets;

public sealed record SocketCreatedResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Address { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required string ApiKey { get; init; }

    public static SocketCreatedResponse From(SocketResponse socket, string apiKey)
        => new()
        {
            Id = socket.Id,
            Name = socket.Name,
            Address = socket.Address,
            CreatedAt = socket.CreatedAt,
            ApiKey = apiKey
        };
}

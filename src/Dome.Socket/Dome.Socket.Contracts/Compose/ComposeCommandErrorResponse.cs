namespace Dome.Socket.Contracts.Compose;

public sealed record ComposeCommandErrorResponse
{
    public required string Message { get; init; }
}

namespace Dome.Socket.Contracts.Images;

public sealed record ImageVersionCheckRequestItem
{
    public required string Image { get; init; }

    public string? ImageId { get; init; }
}

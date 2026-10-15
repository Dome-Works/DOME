namespace Dome.Socket.Contracts.Images;

public sealed record CheckImageVersionsRequest
{
    public IReadOnlyCollection<ImageVersionCheckRequestItem> Images { get; init; }
        = Array.Empty<ImageVersionCheckRequestItem>();
}

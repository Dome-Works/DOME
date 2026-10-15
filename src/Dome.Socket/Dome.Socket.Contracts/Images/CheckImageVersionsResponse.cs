namespace Dome.Socket.Contracts.Images;

public sealed record CheckImageVersionsResponse
{
    public IReadOnlyCollection<ImageVersionCheckResponse> Results { get; init; }
        = Array.Empty<ImageVersionCheckResponse>();
}

namespace Dome.Socket.Contracts.Images;

public sealed record ImageVersionCheckResponse
{
    public required string Image { get; init; }

    public string? ImageId { get; init; }

    public string? LocalDigest { get; init; }

    public string? RemoteDigest { get; init; }

    public IReadOnlyCollection<string> Tags { get; init; } = Array.Empty<string>();

    public bool Unsupported { get; init; }

    public string? Error { get; init; }
}

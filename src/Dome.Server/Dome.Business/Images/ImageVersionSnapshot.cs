namespace Dome.Business.Images;

public sealed record ImageVersionSnapshot
{
    public string? RemoteDigest { get; init; }

    public IReadOnlyCollection<string> Tags { get; init; } = Array.Empty<string>();

    public bool Unsupported { get; init; }

    public string? Error { get; init; }
}

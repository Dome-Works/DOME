namespace Dome.Domain.Images;

public sealed class ImageVersionCache
{
    public Guid Id { get; set; }

    public string ImageReference { get; set; } = string.Empty;

    public string? RemoteDigest { get; set; }

    public string RemoteTags { get; set; } = string.Empty;

    public bool Unsupported { get; set; }

    public string? LastError { get; set; }

    public DateTimeOffset CheckedAt { get; set; }
}

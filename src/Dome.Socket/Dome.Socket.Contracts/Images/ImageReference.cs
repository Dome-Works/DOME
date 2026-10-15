namespace Dome.Socket.Contracts.Images;

public sealed record ImageReference
{
    public required string Host { get; init; }

    public required string Repository { get; init; }

    public string? Tag { get; init; }

    public string? Digest { get; init; }

    public string Canonical
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Tag))
            {
                return $"{Host}/{Repository}:{Tag}";
            }

            if (!string.IsNullOrWhiteSpace(Digest))
            {
                return $"{Host}/{Repository}@{Digest}";
            }

            return $"{Host}/{Repository}";
        }
    }
}

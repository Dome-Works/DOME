namespace Dome.Shared.Images;

public sealed record ImageVersionCheckDto
{
    public required string ContainerId { get; init; }

    public required string Image { get; init; }

    public string? CurrentTag { get; init; }

    public string? NewestTag { get; init; }

    public required string Kind { get; init; }

    public DateTimeOffset? CheckedAt { get; init; }
}

namespace Dome.Business.Images;

public sealed record ImageVersionClassification
{
    public required string Kind { get; init; }

    public string? CurrentTag { get; init; }

    public string? NewestTag { get; init; }
}

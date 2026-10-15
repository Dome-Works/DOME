namespace Dome.Business.Images;

public sealed record ImageTagVersion
{
    public required int Major { get; init; }

    public required int Minor { get; init; }

    public required int Patch { get; init; }

    public required string Suffix { get; init; }

    public required string Original { get; init; }
}

namespace Dome.Shared.Diagrams;

public sealed record DiagramStackDto
{
    public Guid? Id { get; init; }

    public required string ProjectName { get; init; }

    public required string Kind { get; init; }
}

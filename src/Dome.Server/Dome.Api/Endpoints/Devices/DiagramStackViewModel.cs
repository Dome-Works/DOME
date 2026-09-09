using Dome.Shared.Diagrams;

namespace Dome.Api.Endpoints.Devices;

public sealed record DiagramStackViewModel
{
    public Guid? Id { get; init; }

    public required string ProjectName { get; init; }

    public required string Kind { get; init; }

    public static DiagramStackViewModel From(DiagramStackDto stack)
        => new()
        {
            Id = stack.Id,
            ProjectName = stack.ProjectName,
            Kind = stack.Kind
        };
}

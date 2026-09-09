using Dome.Shared.Containers;

namespace Dome.Shared.Diagrams;

public sealed record DeviceDiagramDto
{
    public IReadOnlyList<DiagramStackDto> Stacks { get; init; }
        = Array.Empty<DiagramStackDto>();

    public IReadOnlyCollection<ContainerDto> Containers { get; init; }
        = Array.Empty<ContainerDto>();
}

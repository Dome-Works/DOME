namespace Dome.Api.Endpoints.Devices;

public sealed record GetDeviceDiagramResponse
{
    public IReadOnlyList<DiagramStackViewModel> Stacks { get; init; }
        = Array.Empty<DiagramStackViewModel>();

    public IReadOnlyCollection<ContainerViewModel> Containers { get; init; }
        = Array.Empty<ContainerViewModel>();
}

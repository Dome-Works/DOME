using Dome.Shared.Diagrams;

namespace Dome.Business.Devices;

public interface IDeviceDiagramService
{
    Task<DeviceDiagramDto?> GetDiagramAsync(
        string socketName,
        CancellationToken cancellationToken = default);
}

using Dome.Shared.Containers;
using Dome.Shared.Devices;

namespace Dome.Business.Devices;

public interface IDeviceQueryService
{
    Task<IReadOnlyList<DeviceResponse>> GetDevicesAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ContainerDto>?> GetContainersAsync(
        string deviceName,
        CancellationToken cancellationToken = default);
}

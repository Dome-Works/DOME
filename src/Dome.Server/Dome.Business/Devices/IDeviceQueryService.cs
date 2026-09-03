using HomelabDocs.Shared.Containers;
using HomelabDocs.Shared.Devices;

namespace HomelabDocs.Business.Devices;

public interface IDeviceQueryService
{
    Task<IReadOnlyList<DeviceResponse>> GetDevicesAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ContainerDto>?> GetContainersAsync(
        string deviceName,
        CancellationToken cancellationToken = default);
}

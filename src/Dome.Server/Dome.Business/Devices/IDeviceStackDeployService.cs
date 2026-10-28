namespace Dome.Business.Devices;

public interface IDeviceStackDeployService
{
    Task<DeviceStackDeployResult> DeployAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default);

    Task<DeviceStackDeployResult> DeleteAsync(
        string socketName,
        Guid stackId,
        bool removeVolumes,
        CancellationToken cancellationToken = default);

    Task<DeviceStackDeployResult> DeleteUnmanagedAsync(
        string socketName,
        string projectName,
        bool removeVolumes,
        CancellationToken cancellationToken = default);
}

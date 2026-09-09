namespace Dome.Business.Devices;

public interface IDeviceStackDeployService
{
    Task<DeviceStackDeployResult> DeployAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default);
}

namespace Dome.Business.Devices;

public interface IDeviceContainerCommandService
{
    Task<DeviceContainerCommandResult> StartAsync(
        string deviceName,
        string containerId,
        CancellationToken cancellationToken = default);

    Task<DeviceContainerCommandResult> StopAsync(
        string deviceName,
        string containerId,
        CancellationToken cancellationToken = default);
}

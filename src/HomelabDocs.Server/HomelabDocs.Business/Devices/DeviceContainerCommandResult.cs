namespace HomelabDocs.Business.Devices;

public sealed record DeviceContainerCommandResult
{
    public bool IsDeviceNotFound { get; init; }

    public bool IsContainerNotFound { get; init; }

    public bool IsSuccess => !IsDeviceNotFound && !IsContainerNotFound;

    public static DeviceContainerCommandResult Success() => new();

    public static DeviceContainerCommandResult DeviceNotFound()
        => new() { IsDeviceNotFound = true };

    public static DeviceContainerCommandResult ContainerNotFound()
        => new() { IsContainerNotFound = true };
}

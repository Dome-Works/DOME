namespace Dome.Business.Devices;

public sealed record DeviceStackDeployResult
{
    public bool IsDeviceNotFound { get; init; }

    public bool IsStackNotFound { get; init; }

    public bool IsFailed { get; init; }

    public string? Error { get; init; }

    public bool IsSuccess =>
        !IsDeviceNotFound && !IsStackNotFound && !IsFailed && Error is null;

    public static DeviceStackDeployResult Success() => new();

    public static DeviceStackDeployResult DeviceNotFound()
        => new() { IsDeviceNotFound = true };

    public static DeviceStackDeployResult StackNotFound()
        => new() { IsStackNotFound = true };

    public static DeviceStackDeployResult Failed(string error)
        => new() { IsFailed = true, Error = error };
}

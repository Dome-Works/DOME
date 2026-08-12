namespace HomelabDocs.Shared.Devices;

public sealed record GetDevicesResponse
{
    public IReadOnlyCollection<DeviceResponse> Devices { get; init; }
        = Array.Empty<DeviceResponse>();
}

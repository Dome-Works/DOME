namespace Dome.Api.Endpoints.Devices;

public sealed class UpdateDeviceStackRequest
{
    public string SocketName { get; init; } = string.Empty;

    public Guid StackId { get; init; }

    public string ProjectName { get; init; } = string.Empty;

    public string ComposeYaml { get; init; } = string.Empty;
}

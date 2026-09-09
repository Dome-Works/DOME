namespace Dome.Socket.Contracts.Compose;

public sealed record ComposeUpRequest
{
    public string ProjectName { get; init; } = string.Empty;

    public string ComposeYaml { get; init; } = string.Empty;
}

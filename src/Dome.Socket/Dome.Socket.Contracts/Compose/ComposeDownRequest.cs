namespace Dome.Socket.Contracts.Compose;

public sealed record ComposeDownRequest
{
    public string ProjectName { get; init; } = string.Empty;

    public bool RemoveVolumes { get; init; }
}

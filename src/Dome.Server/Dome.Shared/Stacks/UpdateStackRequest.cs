namespace Dome.Shared.Stacks;

public sealed record UpdateStackRequest
{
    public string ComposeName { get; init; } = string.Empty;

    public string ComposeYaml { get; init; } = string.Empty;
}

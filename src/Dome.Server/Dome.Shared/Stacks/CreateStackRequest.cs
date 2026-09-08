namespace Dome.Shared.Stacks;

public sealed record CreateStackRequest
{
    public string ComposeName { get; init; } = string.Empty;
}

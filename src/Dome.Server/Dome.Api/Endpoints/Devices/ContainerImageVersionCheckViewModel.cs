using Dome.Shared.Images;

namespace Dome.Api.Endpoints.Devices;

public sealed record ContainerImageVersionCheckViewModel
{
    public required string ContainerId { get; init; }

    public required string Image { get; init; }

    public string? CurrentTag { get; init; }

    public string? NewestTag { get; init; }

    public required string Kind { get; init; }

    public DateTimeOffset? CheckedAt { get; init; }

    public static ContainerImageVersionCheckViewModel From(ImageVersionCheckDto check)
        => new()
        {
            ContainerId = check.ContainerId,
            Image = check.Image,
            CurrentTag = check.CurrentTag,
            NewestTag = check.NewestTag,
            Kind = check.Kind,
            CheckedAt = check.CheckedAt,
        };
}

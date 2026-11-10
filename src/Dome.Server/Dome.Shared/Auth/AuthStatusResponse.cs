namespace Dome.Shared.Auth;

public sealed record AuthStatusResponse
{
    public bool SetupRequired { get; init; }

    public bool Authenticated { get; init; }

    public string? Username { get; init; }

    public bool OnboardingCompleted { get; init; }
}

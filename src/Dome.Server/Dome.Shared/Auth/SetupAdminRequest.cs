namespace Dome.Shared.Auth;

public sealed record SetupAdminRequest
{
    public string Username { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}

namespace Dome.Business.Auth;

public sealed record AdminSetupResult
{
    public string? Error { get; init; }

    public bool IsConflict { get; init; }

    public bool IsSuccess => Error is null;

    public static AdminSetupResult Success()
        => new();

    public static AdminSetupResult Invalid(string error)
        => new() { Error = error };

    public static AdminSetupResult Conflict(string error)
        => new() { Error = error, IsConflict = true };
}

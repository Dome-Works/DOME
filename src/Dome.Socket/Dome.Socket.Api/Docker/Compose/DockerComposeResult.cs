namespace Dome.Socket.Api.Docker.Compose;

public sealed record DockerComposeResult
{
    public bool Succeeded { get; init; }

    public bool IsInvalid { get; init; }

    public string? Error { get; init; }

    public static DockerComposeResult Success() => new() { Succeeded = true };

    public static DockerComposeResult Invalid(string error)
        => new() { IsInvalid = true, Error = error };

    public static DockerComposeResult Failed(string error)
        => new() { Succeeded = false, Error = error };
}

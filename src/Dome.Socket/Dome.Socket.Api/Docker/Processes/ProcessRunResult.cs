namespace Dome.Socket.Api.Docker.Processes;

internal sealed record ProcessRunResult
{
    public required int ExitCode { get; init; }

    public required string StandardOutput { get; init; }

    public required string StandardError { get; init; }
}

namespace Dome.Socket.Api.Docker.Processes;

internal interface IProcessRunner
{
    Task<ProcessRunResult> RunAsync(
        string fileName,
        IReadOnlyList<string> arguments,
        string workingDirectory,
        IReadOnlyDictionary<string, string> environment,
        CancellationToken cancellationToken = default);
}

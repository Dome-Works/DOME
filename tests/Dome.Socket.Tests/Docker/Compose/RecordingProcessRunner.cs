using Dome.Socket.Api.Docker.Processes;

namespace Dome.Socket.Tests.Docker.Compose;

internal sealed class RecordingProcessRunner : IProcessRunner
{
    public Exception? Exception { get; set; }

    public ProcessRunResult Result { get; set; } = new()
    {
        ExitCode = 0,
        StandardOutput = string.Empty,
        StandardError = string.Empty
    };

    public int CallCount { get; private set; }

    public string? FileName { get; private set; }

    public IReadOnlyList<string>? Arguments { get; private set; }

    public string? WorkingDirectory { get; private set; }

    public IReadOnlyDictionary<string, string>? Environment { get; private set; }

    public Task<ProcessRunResult> RunAsync(
        string fileName,
        IReadOnlyList<string> arguments,
        string workingDirectory,
        IReadOnlyDictionary<string, string> environment,
        CancellationToken cancellationToken = default)
    {
        CallCount++;
        FileName = fileName;
        Arguments = arguments;
        WorkingDirectory = workingDirectory;
        Environment = environment;

        if (Exception is not null)
        {
            throw Exception;
        }

        return Task.FromResult(Result);
    }
}

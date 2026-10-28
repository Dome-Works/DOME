using System.Text;
using Dome.Socket.Api.Docker.Configuration;
using Dome.Socket.Api.Docker.Processes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dome.Socket.Api.Docker.Compose;

internal sealed class DockerComposeClient : IDockerComposeClient
{
    internal const int MaxComposeYamlBytes = 512 * 1024;
    private const string ComposeFileName = "docker-compose.yml";

    private readonly IProcessRunner _processRunner;
    private readonly DockerEndpointOptions _dockerEndpoint;
    private readonly DockerComposeOptions _composeOptions;
    private readonly ILogger<DockerComposeClient> _logger;

    public DockerComposeClient(
        IProcessRunner processRunner,
        IOptions<DockerEndpointOptions> dockerEndpoint,
        IOptions<DockerComposeOptions> composeOptions,
        ILogger<DockerComposeClient> logger)
    {
        _processRunner = processRunner;
        _dockerEndpoint = dockerEndpoint.Value;
        _composeOptions = composeOptions.Value;
        _logger = logger;
    }

    public async Task<DockerComposeResult> UpAsync(
        string projectName,
        string composeYaml,
        CancellationToken cancellationToken = default)
    {
        if (!ComposeProjectName.IsValid(projectName))
        {
            return DockerComposeResult.Invalid("Project name is not a valid Compose project name.");
        }

        var trimmedProjectName = projectName.Trim();
        var yamlError = ValidateComposeYaml(composeYaml);
        if (yamlError is not null)
        {
            return DockerComposeResult.Invalid(yamlError);
        }

        var projectDirectory = ResolveProjectDirectory(trimmedProjectName);
        if (projectDirectory is null)
        {
            return DockerComposeResult.Invalid("Compose working directory is invalid.");
        }

        Directory.CreateDirectory(projectDirectory);
        var composeFilePath = Path.Join(projectDirectory, ComposeFileName);
        await File.WriteAllTextAsync(composeFilePath, composeYaml, Encoding.UTF8, cancellationToken);

        return await RunComposeAsync(
            trimmedProjectName,
            [
                "compose",
                "--project-name",
                trimmedProjectName,
                "--project-directory",
                projectDirectory,
                "--file",
                composeFilePath,
                "up",
                "--detach"
            ],
            projectDirectory,
            "up",
            cancellationToken);
    }

    public async Task<DockerComposeResult> DownAsync(
        string projectName,
        bool removeVolumes,
        CancellationToken cancellationToken = default)
    {
        if (!ComposeProjectName.IsValid(projectName))
        {
            return DockerComposeResult.Invalid("Project name is not a valid Compose project name.");
        }

        var trimmedProjectName = projectName.Trim();
        var projectDirectory = ResolveProjectDirectory(trimmedProjectName);
        if (projectDirectory is null)
        {
            return DockerComposeResult.Invalid("Compose working directory is invalid.");
        }

        var args = new List<string> { "compose", "--project-name", trimmedProjectName };
        var composeFilePath = Path.Join(projectDirectory, ComposeFileName);
        if (File.Exists(composeFilePath))
        {
            args.AddRange(["--project-directory", projectDirectory, "--file", composeFilePath]);
        }

        args.AddRange(["down", "--remove-orphans"]);
        if (removeVolumes)
        {
            args.Add("--volumes");
        }

        var directoryExists = Directory.Exists(projectDirectory);
        var workingDirectory = directoryExists
            ? projectDirectory
            : Path.GetDirectoryName(projectDirectory)!;

        var result = await RunComposeAsync(
            trimmedProjectName,
            args,
            workingDirectory,
            "down",
            cancellationToken);

        if (result.Succeeded && directoryExists)
        {
            Directory.Delete(projectDirectory, recursive: true);
        }

        return result;
    }

    private async Task<DockerComposeResult> RunComposeAsync(
        string projectName,
        IReadOnlyList<string> args,
        string workingDirectory,
        string verb,
        CancellationToken cancellationToken)
    {
        var dockerCli = string.IsNullOrWhiteSpace(_composeOptions.DockerCliPath)
            ? "docker"
            : _composeOptions.DockerCliPath.Trim();

        ProcessRunResult processResult;
        try
        {
            processResult = await _processRunner.RunAsync(
                dockerCli,
                args,
                workingDirectory,
                new Dictionary<string, string>
                {
                    ["DOCKER_HOST"] = _dockerEndpoint.Endpoint
                },
                cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to start docker compose {Verb} for project '{ProjectName}'.",
                verb,
                projectName);
            return DockerComposeResult.Failed("Docker Compose could not be started.");
        }

        if (processResult.ExitCode == 0)
        {
            return DockerComposeResult.Success();
        }

        var error = CombineOutput(processResult);
        _logger.LogWarning(
            "docker compose {Verb} failed for project '{ProjectName}' with exit code {ExitCode}. {Error}",
            verb,
            projectName,
            processResult.ExitCode,
            error);

        return DockerComposeResult.Failed(
            string.IsNullOrWhiteSpace(error)
                ? $"Docker Compose exited with code {processResult.ExitCode}."
                : error);
    }

    private string? ResolveProjectDirectory(string projectName)
    {
        var root = string.IsNullOrWhiteSpace(_composeOptions.WorkingRoot)
            ? Path.Join(Path.GetTempPath(), "dome", "compose")
            : _composeOptions.WorkingRoot.Trim();

        var fullRoot = Path.GetFullPath(root);
        var projectDirectory = Path.GetFullPath(Path.Join(fullRoot, projectName));
        var prefix = fullRoot.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            + Path.DirectorySeparatorChar;

        if (!projectDirectory.Equals(fullRoot, StringComparison.Ordinal)
            && !projectDirectory.StartsWith(prefix, StringComparison.Ordinal))
        {
            return null;
        }

        return projectDirectory;
    }

    private static string? ValidateComposeYaml(string? composeYaml)
    {
        if (string.IsNullOrWhiteSpace(composeYaml))
        {
            return "Compose file is required.";
        }

        if (Encoding.UTF8.GetByteCount(composeYaml) > MaxComposeYamlBytes)
        {
            return "Compose file must be 512 KiB or smaller.";
        }

        return null;
    }

    private static string CombineOutput(ProcessRunResult result)
    {
        var error = result.StandardError.Trim();
        if (error.Length > 0)
        {
            return error;
        }

        return result.StandardOutput.Trim();
    }
}

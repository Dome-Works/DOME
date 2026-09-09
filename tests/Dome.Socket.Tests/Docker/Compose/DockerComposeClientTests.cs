using Dome.Socket.Api.Docker.Compose;
using Dome.Socket.Api.Docker.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Dome.Socket.Tests.Docker.Compose;

public sealed class DockerComposeClientTests
{
    [Fact]
    public async Task UpAsync_rejects_invalid_project_name()
    {
        var runner = new RecordingProcessRunner();
        var client = CreateClient(runner);

        var result = await client.UpAsync("My Stack", "services: {}", TestContext.Current.CancellationToken);

        Assert.True(result.IsInvalid);
        Assert.Equal(0, runner.CallCount);
    }

    [Fact]
    public async Task UpAsync_rejects_empty_yaml()
    {
        var runner = new RecordingProcessRunner();
        var client = CreateClient(runner);

        var result = await client.UpAsync("app", "  ", TestContext.Current.CancellationToken);

        Assert.True(result.IsInvalid);
        Assert.Equal("Compose file is required.", result.Error);
        Assert.Equal(0, runner.CallCount);
    }

    [Fact]
    public async Task UpAsync_rejects_oversized_yaml()
    {
        var runner = new RecordingProcessRunner();
        var client = CreateClient(runner);
        var yaml = new string('a', DockerComposeClient.MaxComposeYamlBytes + 1);

        var result = await client.UpAsync("app", yaml, TestContext.Current.CancellationToken);

        Assert.True(result.IsInvalid);
        Assert.Equal("Compose file must be 512 KiB or smaller.", result.Error);
        Assert.Equal(0, runner.CallCount);
    }

    [Fact]
    public async Task UpAsync_writes_compose_file_and_runs_docker_compose()
    {
        var workingRoot = CreateWorkingRoot();
        var runner = new RecordingProcessRunner();
        var client = CreateClient(runner, workingRoot);

        var result = await client.UpAsync("my-stack", "services: {}\n", TestContext.Current.CancellationToken);

        Assert.True(result.Succeeded);
        Assert.Equal("docker", runner.FileName);
        Assert.Equal(
            [
                "compose",
                "--project-name",
                "my-stack",
                "--project-directory",
                Path.GetFullPath(Path.Join(workingRoot, "my-stack")),
                "--file",
                Path.GetFullPath(Path.Join(workingRoot, "my-stack", "docker-compose.yml")),
                "up",
                "--detach"
            ],
            runner.Arguments);
        Assert.Equal("unix:///var/run/docker.sock", runner.Environment?["DOCKER_HOST"]);
        Assert.Equal(
            "services: {}\n",
            await File.ReadAllTextAsync(
                Path.Join(workingRoot, "my-stack", "docker-compose.yml"),
                TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task UpAsync_returns_failed_when_compose_exits_nonzero()
    {
        var runner = new RecordingProcessRunner
        {
            Result = new()
            {
                ExitCode = 1,
                StandardOutput = string.Empty,
                StandardError = "service 'web' has neither an image nor a build context specified"
            }
        };
        var client = CreateClient(runner, CreateWorkingRoot());

        var result = await client.UpAsync("app", "services: {}\n", TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.False(result.IsInvalid);
        Assert.Contains("neither an image", result.Error);
    }

    [Fact]
    public async Task UpAsync_returns_failed_when_process_cannot_start()
    {
        var runner = new RecordingProcessRunner
        {
            Exception = new InvalidOperationException("Failed to start 'docker'.")
        };
        var client = CreateClient(runner, CreateWorkingRoot());

        var result = await client.UpAsync("app", "services: {}\n", TestContext.Current.CancellationToken);

        Assert.False(result.Succeeded);
        Assert.Equal("Docker Compose could not be started.", result.Error);
    }

    private static DockerComposeClient CreateClient(
        RecordingProcessRunner runner,
        string? workingRoot = null)
        => new(
            runner,
            Options.Create(new DockerEndpointOptions()),
            Options.Create(new DockerComposeOptions
            {
                WorkingRoot = workingRoot ?? CreateWorkingRoot(),
                DockerCliPath = "docker"
            }),
            NullLogger<DockerComposeClient>.Instance);

    private static string CreateWorkingRoot()
        => Path.Join(Path.GetTempPath(), "dome-compose-tests", Guid.NewGuid().ToString("N"));
}

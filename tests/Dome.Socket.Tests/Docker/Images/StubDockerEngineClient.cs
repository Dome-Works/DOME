using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Contracts.Containers;

namespace Dome.Socket.Tests.Docker.Images;

internal sealed class StubDockerEngineClient : IDockerEngineClient
{
    public Task<bool> IsReachableAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(true);

    public Task<IReadOnlyCollection<ContainerResponse>> ListContainersAsync(
        CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyCollection<ContainerResponse>>([]);

    public Task<IReadOnlyList<string>> GetImageRepoDigestsAsync(
        string imageId,
        CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<string>>(["prometheus@sha256:from-inspect"]);

    public Task<DockerContainerLifecycleResult> StartContainerAsync(
        string containerId,
        CancellationToken cancellationToken = default)
        => Task.FromResult(DockerContainerLifecycleResult.Succeeded);

    public Task<DockerContainerLifecycleResult> StopContainerAsync(
        string containerId,
        CancellationToken cancellationToken = default)
        => Task.FromResult(DockerContainerLifecycleResult.Succeeded);
}

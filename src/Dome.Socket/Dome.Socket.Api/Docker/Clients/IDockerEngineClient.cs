using HomelabDocs.Socket.Contracts.Containers;

namespace HomelabDocs.Socket.Api.Docker.Clients;

public interface IDockerEngineClient
{
    Task<bool> IsReachableAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ContainerResponse>> ListContainersAsync(
        CancellationToken cancellationToken = default);

    Task<DockerContainerLifecycleResult> StartContainerAsync(
        string containerId,
        CancellationToken cancellationToken = default);

    Task<DockerContainerLifecycleResult> StopContainerAsync(
        string containerId,
        CancellationToken cancellationToken = default);
}

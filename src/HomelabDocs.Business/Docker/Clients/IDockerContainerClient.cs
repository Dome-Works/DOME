using HomelabDocs.Shared.Containers;

namespace HomelabDocs.Business.Docker.Clients;

public interface IDockerContainerClient
{
    Task<IReadOnlyCollection<ContainerResponse>> GetRunningContainersAsync(
        CancellationToken cancellationToken = default);
}

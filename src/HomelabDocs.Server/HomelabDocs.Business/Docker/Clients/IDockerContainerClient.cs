using HomelabDocs.Shared.Containers;

namespace HomelabDocs.Business.Docker.Clients;

public interface IDockerContainerClient
{
    Task<IReadOnlyCollection<ContainerResponse>> GetRunningContainersAsync(
        string deviceName,
        CancellationToken cancellationToken = default);
}

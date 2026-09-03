using Dome.Socket.Contracts.Containers;
using Dome.Socket.Contracts.Health;
using Refit;

namespace Dome.Socket.Contracts;

public interface IDomeSocketApi
{
    [Get("/api/health")]
    Task<SocketHealthResponse> GetHealthAsync(CancellationToken cancellationToken = default);

    [Get("/api/containers")]
    Task<GetContainersResponse> GetContainersAsync(CancellationToken cancellationToken = default);

    [Post("/api/containers/{id}/start")]
    Task StartContainerAsync(string id, CancellationToken cancellationToken = default);

    [Post("/api/containers/{id}/stop")]
    Task StopContainerAsync(string id, CancellationToken cancellationToken = default);
}

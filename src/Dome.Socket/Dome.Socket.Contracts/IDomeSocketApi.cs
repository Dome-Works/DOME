using Dome.Socket.Contracts.Compose;
using Dome.Socket.Contracts.Containers;
using Dome.Socket.Contracts.Health;
using Dome.Socket.Contracts.Images;
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

    [Post("/api/containers/delete")]
    Task<DeleteContainersResponse> DeleteContainersAsync(
        [Body] DeleteContainersRequest request,
        CancellationToken cancellationToken = default);

    [Post("/api/compose/up")]
    Task UpComposeAsync(
        [Body] ComposeUpRequest request,
        CancellationToken cancellationToken = default);

    [Post("/api/images/version-checks")]
    Task<CheckImageVersionsResponse> CheckImageVersionsAsync(
        [Body] CheckImageVersionsRequest request,
        CancellationToken cancellationToken = default);
}

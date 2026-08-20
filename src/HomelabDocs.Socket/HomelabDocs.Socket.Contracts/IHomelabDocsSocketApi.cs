using HomelabDocs.Socket.Contracts.Containers;
using HomelabDocs.Socket.Contracts.Health;
using Refit;

namespace HomelabDocs.Socket.Contracts;

public interface IHomelabDocsSocketApi
{
    [Get("/api/health")]
    Task<SocketHealthResponse> GetHealthAsync(CancellationToken cancellationToken = default);

    [Get("/api/containers")]
    Task<GetContainersResponse> GetContainersAsync(CancellationToken cancellationToken = default);
}

using HomelabDocs.Shared.Containers;
using Refit;

namespace HomelabDocs.Shared.Api;

public interface IHomelabDocsApi
{
    [Get("/api/containers")]
    Task<GetRunningContainersResponse> GetRunningContainersAsync(
        CancellationToken cancellationToken = default);
}

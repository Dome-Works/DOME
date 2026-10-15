using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Docker.Images;

public interface IImageVersionCheckService
{
    Task<CheckImageVersionsResponse> CheckAsync(
        CheckImageVersionsRequest request,
        CancellationToken cancellationToken = default);
}

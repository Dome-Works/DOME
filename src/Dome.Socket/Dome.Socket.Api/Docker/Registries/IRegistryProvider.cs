using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Docker.Registries;

internal interface IRegistryProvider
{
    bool CanHandle(ImageReference reference);

    Task<RegistryVersionInfo> GetVersionInfoAsync(
        ImageReference reference,
        CancellationToken cancellationToken = default);
}

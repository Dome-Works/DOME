using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Docker.Registries;

internal sealed class DockerHubRegistryProvider : OciRegistryProvider
{
    public DockerHubRegistryProvider(HttpClient httpClient)
        : base(httpClient)
    {
    }

    protected override string RegistryApiHost => "registry-1.docker.io";

    protected override string DefaultTokenRealm => "https://auth.docker.io/token";

    protected override string DefaultTokenService => "registry.docker.io";

    public override bool CanHandle(ImageReference reference)
    {
        ArgumentNullException.ThrowIfNull(reference);
        return reference.Host.Equals(
            ImageReferenceParser.DockerHubHost,
            StringComparison.OrdinalIgnoreCase);
    }
}

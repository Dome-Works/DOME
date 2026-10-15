using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Docker.Registries;

internal sealed class GhcrRegistryProvider : OciRegistryProvider
{
    private const string Host = "ghcr.io";

    public GhcrRegistryProvider(HttpClient httpClient)
        : base(httpClient)
    {
    }

    protected override string RegistryApiHost => Host;

    protected override string DefaultTokenRealm => "https://ghcr.io/token";

    protected override string DefaultTokenService => Host;

    public override bool CanHandle(ImageReference reference)
    {
        ArgumentNullException.ThrowIfNull(reference);
        return reference.Host.Equals(Host, StringComparison.OrdinalIgnoreCase);
    }
}

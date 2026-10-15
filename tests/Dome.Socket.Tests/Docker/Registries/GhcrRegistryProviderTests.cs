using Dome.Socket.Api.Docker.Registries;
using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Tests.Docker.Registries;

public sealed class GhcrRegistryProviderTests
{
    [Fact]
    public void CanHandle_github_container_registry_images()
    {
        var provider = new GhcrRegistryProvider(new HttpClient());

        Assert.True(provider.CanHandle(Parse("ghcr.io/dome-works/dome:1.0.0")));
        Assert.False(provider.CanHandle(Parse("nginx:1.27.3")));
        Assert.False(provider.CanHandle(Parse("quay.io/prometheus/prometheus:v2.50.0")));
    }

    private static ImageReference Parse(string image)
    {
        var reference = ImageReferenceParser.TryParse(image);
        Assert.NotNull(reference);
        return reference;
    }
}

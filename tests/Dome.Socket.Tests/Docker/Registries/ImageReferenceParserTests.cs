using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Tests.Docker.Registries;

public sealed class ImageReferenceParserTests
{
    [Theory]
    [InlineData("nginx", "docker.io", "library/nginx", "latest", null)]
    [InlineData("nginx:1.27.3", "docker.io", "library/nginx", "1.27.3", null)]
    [InlineData("library/nginx:1.27.3", "docker.io", "library/nginx", "1.27.3", null)]
    [InlineData("user/app:2.0", "docker.io", "user/app", "2.0", null)]
    [InlineData("docker.io/library/nginx:latest", "docker.io", "library/nginx", "latest", null)]
    [InlineData("ghcr.io/dome-works/dome:1.2.3", "ghcr.io", "dome-works/dome", "1.2.3", null)]
    [InlineData("nginx@sha256:deadbeef", "docker.io", "library/nginx", null, "sha256:deadbeef")]
    [InlineData(
        "ghcr.io/dome-works/dome:1.2.3@sha256:abcd",
        "ghcr.io",
        "dome-works/dome",
        "1.2.3",
        "sha256:abcd")]
    [InlineData("quay.io/prometheus/prometheus:v2", "quay.io", "prometheus/prometheus", "v2", null)]
    public void TryParse_canonicalizes_common_references(
        string image,
        string host,
        string repository,
        string? tag,
        string? digest)
    {
        var parsed = ImageReferenceParser.TryParse(image);

        Assert.NotNull(parsed);
        Assert.Equal(host, parsed.Host);
        Assert.Equal(repository, parsed.Repository);
        Assert.Equal(tag, parsed.Tag);
        Assert.Equal(digest, parsed.Digest);
    }

    [Fact]
    public void TryParse_builds_canonical_name_for_hub_and_ghcr()
    {
        var hub = ImageReferenceParser.TryParse("nginx:1.27.3");
        var ghcr = ImageReferenceParser.TryParse("ghcr.io/dome-works/dome:1.2.3");

        Assert.Equal("docker.io/library/nginx:1.27.3", hub?.Canonical);
        Assert.Equal("ghcr.io/dome-works/dome:1.2.3", ghcr?.Canonical);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TryParse_returns_null_for_blank_values(string? image)
    {
        Assert.Null(ImageReferenceParser.TryParse(image));
    }
}

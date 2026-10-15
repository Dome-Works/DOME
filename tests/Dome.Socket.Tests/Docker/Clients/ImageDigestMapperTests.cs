using Dome.Socket.Api.Docker.Clients;

namespace Dome.Socket.Tests.Docker.Clients;

public sealed class ImageDigestMapperTests
{
    [Fact]
    public void Resolve_prefers_digest_embedded_in_image_reference()
    {
        var digest = ImageDigestMapper.Resolve(
            "nginx:1.27.3@sha256:deadbeef",
            ["nginx@sha256:other"]);

        Assert.Equal("sha256:deadbeef", digest);
    }

    [Fact]
    public void Resolve_selects_repo_digest_matching_image_name()
    {
        var digest = ImageDigestMapper.Resolve(
            "ghcr.io/dome-works/dome:latest",
            [
                "unrelated/app@sha256:aaaa",
                "ghcr.io/dome-works/dome@sha256:bbbb",
            ]);

        Assert.Equal("sha256:bbbb", digest);
    }

    [Fact]
    public void Resolve_falls_back_to_first_repo_digest()
    {
        var digest = ImageDigestMapper.Resolve(
            "nginx:latest",
            ["nginx@sha256:cccc"]);

        Assert.Equal("sha256:cccc", digest);
    }

    [Fact]
    public void Resolve_returns_null_when_no_digest_is_available()
    {
        Assert.Null(ImageDigestMapper.Resolve("nginx:latest", null));
        Assert.Null(ImageDigestMapper.Resolve("nginx:latest", Array.Empty<string>()));
    }
}

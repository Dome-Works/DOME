using Dome.Business.Images;
using Dome.Shared.Images;

namespace Dome.Business.Tests.Images;

public sealed class ImageVersionClassifierTests
{
    [Fact]
    public void Classify_returns_unknown_when_registry_is_unsupported()
    {
        var result = ImageVersionClassifier.Classify(
            "quay.io/prometheus/prometheus:v2.50.0",
            "sha256:local",
            new ImageVersionSnapshot
            {
                Unsupported = true,
            });

        Assert.Equal(ImageVersionKind.Unknown, result.Kind);
    }

    [Fact]
    public void Classify_returns_major_when_a_newer_major_tag_exists()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:1.27.3",
            "sha256:local",
            SuccessSnapshot("sha256:local", "1.27.4", "1.28.0", "2.0.0"));

        Assert.Equal(ImageVersionKind.Major, result.Kind);
        Assert.Equal("1.27.3", result.CurrentTag);
        Assert.Equal("2.0.0", result.NewestTag);
    }

    [Fact]
    public void Classify_returns_minor_when_newest_in_channel_is_minor()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:1.27.3",
            "sha256:local",
            SuccessSnapshot("sha256:local", "1.27.4", "1.28.1"));

        Assert.Equal(ImageVersionKind.Minor, result.Kind);
        Assert.Equal("1.28.1", result.NewestTag);
    }

    [Fact]
    public void Classify_returns_patch_when_only_patch_is_newer()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:1.27.3",
            "sha256:local",
            SuccessSnapshot("sha256:local", "1.27.3", "1.27.4"));

        Assert.Equal(ImageVersionKind.Patch, result.Kind);
        Assert.Equal("1.27.4", result.NewestTag);
    }

    [Fact]
    public void Classify_ignores_tags_with_a_different_suffix()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:1.27.3-alpine",
            "sha256:local",
            SuccessSnapshot("sha256:local", "1.27.4", "1.27.4-alpine", "2.0.0"));

        Assert.Equal(ImageVersionKind.Patch, result.Kind);
        Assert.Equal("1.27.4-alpine", result.NewestTag);
    }

    [Fact]
    public void Classify_returns_digest_when_floating_tag_moved()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:latest",
            "sha256:old",
            SuccessSnapshot("sha256:new", "latest", "1.27.3"));

        Assert.Equal(ImageVersionKind.Digest, result.Kind);
        Assert.Equal("latest", result.CurrentTag);
        Assert.Equal("latest", result.NewestTag);
    }

    [Fact]
    public void Classify_returns_none_when_floating_tag_digest_matches()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:latest",
            "sha256:same",
            SuccessSnapshot("sha256:same", "latest"));

        Assert.Equal(ImageVersionKind.None, result.Kind);
    }

    [Fact]
    public void Classify_returns_digest_when_semver_tag_was_rebuilt()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:1.27.3",
            "sha256:old",
            SuccessSnapshot("sha256:new", "1.27.3"));

        Assert.Equal(ImageVersionKind.Digest, result.Kind);
        Assert.Equal("1.27.3", result.NewestTag);
    }

    [Fact]
    public void Classify_returns_unknown_when_check_failed_without_remote_data()
    {
        var result = ImageVersionClassifier.Classify(
            "nginx:1.27.3",
            "sha256:local",
            new ImageVersionSnapshot
            {
                Error = "rate limited",
            });

        Assert.Equal(ImageVersionKind.Unknown, result.Kind);
    }

    private static ImageVersionSnapshot SuccessSnapshot(string remoteDigest, params string[] tags)
        => new()
        {
            RemoteDigest = remoteDigest,
            Tags = tags,
        };
}

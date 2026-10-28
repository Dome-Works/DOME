using FakeItEasy;
using Dome.Business.Settings;
using Dome.Domain.Settings;

namespace Dome.Business.Tests.Settings;

public sealed class SystemSettingsServiceTests
{
    [Theory]
    [InlineData("True", "id", true)]
    [InlineData("true", "id", true)]
    [InlineData("False", "id", false)]
    [InlineData(null, "id", false)]
    [InlineData("yes", "id", false)]
    [InlineData("True", null, false)]
    [InlineData("True", "", false)]
    public async Task GetTelemetryAsync_is_enabled_only_for_true_with_an_installation_id(
        string? enabled,
        string? installationId,
        bool expected)
    {
        var repository = A.Fake<ISystemSettingRepository>();
        A.CallTo(() => repository.GetAsync(SystemSettingKeys.TelemetryEnabled, A<CancellationToken>._))
            .Returns(enabled);
        A.CallTo(() => repository.GetAsync(SystemSettingKeys.InstallationId, A<CancellationToken>._))
            .Returns(installationId);

        var result = await new SystemSettingsService(repository).GetTelemetryAsync(TestContext.Current.CancellationToken);

        Assert.Equal(expected, result.Enabled);
        Assert.Equal(installationId ?? string.Empty, result.InstallationId);
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public async Task SetTelemetryEnabledAsync_stores_the_flag(bool enabled, string expected)
    {
        var repository = A.Fake<ISystemSettingRepository>();

        await new SystemSettingsService(repository).SetTelemetryEnabledAsync(enabled, TestContext.Current.CancellationToken);

        A.CallTo(() => repository.SetAsync(SystemSettingKeys.TelemetryEnabled, expected, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Theory]
    [InlineData("True", true)]
    [InlineData("False", false)]
    [InlineData(null, false)]
    [InlineData("yes", false)]
    public async Task GetOnboardingCompletedAsync_is_true_only_for_true(string? completed, bool expected)
    {
        var repository = A.Fake<ISystemSettingRepository>();
        A.CallTo(() => repository.GetAsync(SystemSettingKeys.OnboardingCompleted, A<CancellationToken>._))
            .Returns(completed);

        var result = await new SystemSettingsService(repository).GetOnboardingCompletedAsync(TestContext.Current.CancellationToken);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task SetOnboardingCompletedAsync_stores_the_flag()
    {
        var repository = A.Fake<ISystemSettingRepository>();

        await new SystemSettingsService(repository).SetOnboardingCompletedAsync(true, TestContext.Current.CancellationToken);

        A.CallTo(() => repository.SetAsync(SystemSettingKeys.OnboardingCompleted, "True", A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}

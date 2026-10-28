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

        var result = await new SystemSettingsService(repository).GetTelemetryAsync();

        Assert.Equal(expected, result.Enabled);
        Assert.Equal(installationId ?? string.Empty, result.InstallationId);
    }
}

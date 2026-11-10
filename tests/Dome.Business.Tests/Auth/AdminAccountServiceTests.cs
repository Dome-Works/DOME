using Dome.Business.Auth;
using Dome.Domain.Settings;
using FakeItEasy;

namespace Dome.Business.Tests.Auth;

public sealed class AdminAccountServiceTests
{
    private const string Password = "correct-horse-battery";

    private static AdminAccountService CreateService()
    {
        var store = new Dictionary<string, string>();
        var repository = A.Fake<ISystemSettingRepository>();
        A.CallTo(() => repository.GetAsync(A<string>._, A<CancellationToken>._))
            .ReturnsLazily((string key, CancellationToken _) => store.GetValueOrDefault(key));
        A.CallTo(() => repository.SetAsync(A<string>._, A<string>._, A<CancellationToken>._))
            .Invokes((string key, string value, CancellationToken _) => store[key] = value);
        return new AdminAccountService(repository);
    }

    [Fact]
    public async Task Setup_is_required_initially()
    {
        Assert.True(await CreateService().IsSetupRequiredAsync(TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Setup_succeeds_once_then_conflicts()
    {
        var service = CreateService();
        var ct = TestContext.Current.CancellationToken;

        Assert.True((await service.SetupAsync(" admin ", Password, ct)).IsSuccess);
        Assert.False(await service.IsSetupRequiredAsync(ct));

        var second = await service.SetupAsync("other", Password, ct);
        Assert.False(second.IsSuccess);
        Assert.True(second.IsConflict);
    }

    [Theory]
    [InlineData("admin", "short")]
    [InlineData("   ", Password)]
    public async Task Setup_rejects_invalid_input(string username, string password)
    {
        var service = CreateService();
        var ct = TestContext.Current.CancellationToken;

        var result = await service.SetupAsync(username, password, ct);

        Assert.False(result.IsSuccess);
        Assert.False(result.IsConflict);
        Assert.True(await service.IsSetupRequiredAsync(ct));
    }

    [Fact]
    public async Task Setup_rejects_long_username()
    {
        var result = await CreateService().SetupAsync(new string('a', 65), Password, TestContext.Current.CancellationToken);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData("admin", Password, "admin")]
    [InlineData("ADMIN", Password, "admin")]
    [InlineData("admin", "wrong-password-123", null)]
    [InlineData("someone", Password, null)]
    public async Task Validate_returns_username_only_for_correct_credentials(string username, string password, string? expected)
    {
        var service = CreateService();
        var ct = TestContext.Current.CancellationToken;
        await service.SetupAsync("admin", Password, ct);

        Assert.Equal(expected, await service.ValidateAsync(username, password, ct));
    }

    [Fact]
    public async Task Validate_without_admin_returns_null()
    {
        Assert.Null(await CreateService().ValidateAsync("admin", Password, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Reset_requires_setup_again()
    {
        var service = CreateService();
        var ct = TestContext.Current.CancellationToken;
        await service.SetupAsync("admin", Password, ct);

        await service.ResetAsync(ct);

        Assert.True(await service.IsSetupRequiredAsync(ct));
        Assert.Null(await service.ValidateAsync("admin", Password, ct));
    }
}

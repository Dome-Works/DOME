using Dome.Domain.Settings;

namespace Dome.Business.Auth;

internal sealed class AdminAccountService : IAdminAccountService
{
    private const int MaxUsernameLength = 64;
    private const int MinPasswordLength = 12;
    private static readonly string DummyHash = PasswordHasher.Hash(Guid.NewGuid().ToString());

    private readonly ISystemSettingRepository _repository;

    public AdminAccountService(ISystemSettingRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsSetupRequiredAsync(CancellationToken cancellationToken = default)
        => string.IsNullOrEmpty(await _repository.GetAsync(SystemSettingKeys.AdminPasswordHash, cancellationToken));

    public async Task<AdminSetupResult> SetupAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        if (!await IsSetupRequiredAsync(cancellationToken))
            return AdminSetupResult.Conflict("An admin account already exists.");

        var trimmed = username?.Trim() ?? string.Empty;
        if (trimmed.Length == 0)
            return AdminSetupResult.Invalid("Username is required.");
        if (trimmed.Length > MaxUsernameLength)
            return AdminSetupResult.Invalid($"Username must be at most {MaxUsernameLength} characters.");
        if (password is null || password.Length < MinPasswordLength)
            return AdminSetupResult.Invalid($"Password must be at least {MinPasswordLength} characters.");

        await _repository.SetAsync(SystemSettingKeys.AdminUsername, trimmed, cancellationToken);
        await _repository.SetAsync(SystemSettingKeys.AdminPasswordHash, PasswordHasher.Hash(password), cancellationToken);
        return AdminSetupResult.Success();
    }

    public async Task<string?> ValidateAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var storedUsername = await _repository.GetAsync(SystemSettingKeys.AdminUsername, cancellationToken);
        var storedHash = await _repository.GetAsync(SystemSettingKeys.AdminPasswordHash, cancellationToken);
        var hasAdmin = !string.IsNullOrEmpty(storedUsername) && !string.IsNullOrEmpty(storedHash);

        var passwordValid = PasswordHasher.Verify(password ?? string.Empty, hasAdmin ? storedHash! : DummyHash);
        var usernameValid = hasAdmin && string.Equals(username?.Trim(), storedUsername, StringComparison.OrdinalIgnoreCase);

        return passwordValid && usernameValid ? storedUsername : null;
    }

    public async Task ResetAsync(CancellationToken cancellationToken = default)
    {
        await _repository.SetAsync(SystemSettingKeys.AdminPasswordHash, string.Empty, cancellationToken);
        await _repository.SetAsync(SystemSettingKeys.AdminUsername, string.Empty, cancellationToken);
    }
}

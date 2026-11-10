namespace Dome.Business.Auth;

public interface IAdminAccountService
{
    Task<bool> IsSetupRequiredAsync(CancellationToken cancellationToken = default);

    Task<AdminSetupResult> SetupAsync(string username, string password, CancellationToken cancellationToken = default);

    Task<string?> ValidateAsync(string username, string password, CancellationToken cancellationToken = default);

    Task ResetAsync(CancellationToken cancellationToken = default);
}

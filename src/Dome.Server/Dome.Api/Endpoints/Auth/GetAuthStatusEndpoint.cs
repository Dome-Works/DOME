using FastEndpoints;
using Dome.Business.Auth;
using Dome.Business.Settings;
using Dome.Shared.Auth;

namespace Dome.Api.Endpoints.Auth;

public sealed class GetAuthStatusEndpoint : EndpointWithoutRequest<AuthStatusResponse>
{
    private readonly IAdminAccountService _adminAccountService;
    private readonly ISystemSettingsService _systemSettingsService;

    public GetAuthStatusEndpoint(
        IAdminAccountService adminAccountService,
        ISystemSettingsService systemSettingsService)
    {
        _adminAccountService = adminAccountService;
        _systemSettingsService = systemSettingsService;
    }

    public override void Configure()
    {
        Get("/api/auth/status");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var authenticated = User.Identity?.IsAuthenticated == true;
        await Send.OkAsync(
            new AuthStatusResponse
            {
                SetupRequired = await _adminAccountService.IsSetupRequiredAsync(ct),
                Authenticated = authenticated,
                Username = authenticated ? User.Identity!.Name : null,
                OnboardingCompleted = await _systemSettingsService.GetOnboardingCompletedAsync(ct)
            },
            ct);
    }
}

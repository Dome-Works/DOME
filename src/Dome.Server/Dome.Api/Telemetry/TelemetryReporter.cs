using Dome.Business;
using Dome.Business.Settings;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using PostHog;

namespace Dome.Api.Telemetry;

/// <summary>
/// Sends daily heartbeat events so PostHog can count unique Server instances,
/// Sockets and Stacks per week. Which data is sent is documented in the README.
/// </summary>
public sealed class TelemetryReporter : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IPostHogClient _posthog;
    private readonly IApplicationVersion _applicationVersion;
    private readonly ILogger<TelemetryReporter> _logger;

    public TelemetryReporter(
        IServiceScopeFactory scopeFactory,
        IPostHogClient posthog,
        IApplicationVersion applicationVersion,
        ILogger<TelemetryReporter> logger)
    {
        _scopeFactory = scopeFactory;
        _posthog = posthog;
        _applicationVersion = applicationVersion;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Interval);
        do
        {
            await ReportAsync(stoppingToken);
        }
        while (await WaitAsync(timer, stoppingToken));
    }

    private static async Task<bool> WaitAsync(PeriodicTimer timer, CancellationToken cancellationToken)
    {
        try
        {
            return await timer.WaitForNextTickAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }

    private async Task ReportAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var telemetry = await scope.ServiceProvider
                .GetRequiredService<ISystemSettingsService>()
                .GetTelemetryAsync(cancellationToken);
            if (!telemetry.Enabled)
            {
                return;
            }

            var sockets = await scope.ServiceProvider
                .GetRequiredService<ISocketRepository>()
                .ListAsync(cancellationToken);
            var stackRepository = scope.ServiceProvider.GetRequiredService<IStackRepository>();
            var stacks = new List<Stack>();
            foreach (var socket in sockets)
            {
                stacks.AddRange(await stackRepository.ListBySocketIdAsync(socket.Id, cancellationToken));
            }

            Capture(telemetry.InstallationId, "instance_heartbeat", new()
            {
                ["socket_count"] = sockets.Count,
                ["stack_count"] = stacks.Count,
            });
            foreach (var socket in sockets)
            {
                Capture(telemetry.InstallationId, "socket_heartbeat", new() { ["socket_id"] = socket.Id });
            }

            foreach (var stack in stacks)
            {
                Capture(telemetry.InstallationId, "stack_heartbeat", new() { ["stack_id"] = stack.Id });
            }
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            // Telemetry must never affect the app.
            _logger.LogDebug(exception, "Failed to report telemetry.");
        }
    }

    private void Capture(string installationId, string eventName, Dictionary<string, object> properties)
    {
        properties["environment"] = TelemetryBuildInfo.Environment;
        properties["app_version"] = _applicationVersion.Current;
        _posthog.Capture(installationId, eventName, properties: properties);
    }
}

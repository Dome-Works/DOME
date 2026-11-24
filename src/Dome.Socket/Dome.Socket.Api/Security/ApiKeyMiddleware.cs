using System.Security.Cryptography;
using System.Text;

namespace Dome.Socket.Api.Security;

public sealed class ApiKeyMiddleware
{
    private const string BearerPrefix = "Bearer ";
    private readonly RequestDelegate _next;
    private readonly SocketSecurity _security;
    private readonly SocketKey _socketKey;
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(
        RequestDelegate next,
        SocketSecurity security,
        SocketKey socketKey,
        ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _security = security;
        _socketKey = socketKey;
        _logger = logger;
    }

    public Task InvokeAsync(HttpContext context)
    {
        if (IsExempt(context.Request))
        {
            return _next(context);
        }

        if (IsAuthorized(context.Request))
        {
            if (_socketKey.MarkPaired())
            {
                _logger.LogInformation("Paired with DOME. The socket key is no longer shown at startup.");
            }

            return _next(context);
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }

    private static bool IsExempt(HttpRequest request) =>
        HttpMethods.IsPost(request.Method) &&
        request.Path.Equals("/api/pair", StringComparison.OrdinalIgnoreCase);

    private bool IsAuthorized(HttpRequest request)
    {
        if (_security.Key.Length == 0)
        {
            return false;
        }

        var header = request.Headers.Authorization.ToString();
        if (!header.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var provided = Encoding.UTF8.GetBytes(header[BearerPrefix.Length..]);
        return CryptographicOperations.FixedTimeEquals(provided, _security.Key);
    }
}

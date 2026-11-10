using System.Security.Cryptography;
using System.Text;

namespace Dome.Socket.Api.Security;

public sealed class ApiKeyMiddleware
{
    private const string BearerPrefix = "Bearer ";
    private readonly RequestDelegate _next;
    private readonly SocketSecurity _security;

    public ApiKeyMiddleware(RequestDelegate next, SocketSecurity security)
    {
        _next = next;
        _security = security;
    }

    public Task InvokeAsync(HttpContext context)
    {
        if (IsExempt(context.Request) || IsAuthorized(context.Request))
        {
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
        if (!header.StartsWith(BearerPrefix, StringComparison.Ordinal))
        {
            return false;
        }

        var provided = Encoding.UTF8.GetBytes(header[BearerPrefix.Length..]);
        return CryptographicOperations.FixedTimeEquals(provided, _security.Key);
    }
}

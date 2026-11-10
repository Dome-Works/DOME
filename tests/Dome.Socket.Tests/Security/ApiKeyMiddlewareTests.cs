using System.Text;
using Dome.Socket.Api.Security;
using Microsoft.AspNetCore.Http;

namespace Dome.Socket.Tests.Security;

public sealed class ApiKeyMiddlewareTests
{
    private static async Task<(int Status, bool NextCalled)> InvokeAsync(
        string configuredKey, string method, string path, string? authorization)
    {
        var nextCalled = false;
        var security = new SocketSecurity(null!, [], Encoding.UTF8.GetBytes(configuredKey));
        var middleware = new ApiKeyMiddleware(_ => { nextCalled = true; return Task.CompletedTask; }, security);
        var context = new DefaultHttpContext();
        context.Request.Method = method;
        context.Request.Path = path;
        if (authorization is not null)
        {
            context.Request.Headers.Authorization = authorization;
        }

        await middleware.InvokeAsync(context);
        return (context.Response.StatusCode, nextCalled);
    }

    [Fact]
    public async Task MissingHeader_Returns401()
    {
        var result = await InvokeAsync("secret", "GET", "/api/health", null);
        Assert.Equal((401, false), result);
    }

    [Fact]
    public async Task WrongKey_Returns401()
    {
        var result = await InvokeAsync("secret", "GET", "/api/health", "Bearer wrong-and-longer");
        Assert.Equal((401, false), result);
    }

    [Fact]
    public async Task RightKey_CallsNext()
    {
        var result = await InvokeAsync("secret", "GET", "/api/health", "Bearer secret");
        Assert.True(result.NextCalled);
    }

    [Fact]
    public async Task PostPair_WithoutHeader_CallsNext()
    {
        var result = await InvokeAsync("secret", "POST", "/api/pair", null);
        Assert.True(result.NextCalled);
    }

    [Fact]
    public async Task GetPair_WithoutHeader_Returns401()
    {
        var result = await InvokeAsync("secret", "GET", "/api/pair", null);
        Assert.Equal((401, false), result);
    }

    [Fact]
    public async Task EmptyConfiguredKey_WithEmptyBearer_Returns401()
    {
        var result = await InvokeAsync("", "GET", "/api/health", "Bearer ");
        Assert.Equal((401, false), result);
    }
}

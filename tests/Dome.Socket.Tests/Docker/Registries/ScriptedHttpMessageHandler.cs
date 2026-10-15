namespace Dome.Socket.Tests.Docker.Registries;

internal sealed class ScriptedHttpMessageHandler : HttpMessageHandler
{
    private readonly Queue<Func<HttpRequestMessage, HttpResponseMessage>> _scripts = new();

    public void Enqueue(Func<HttpRequestMessage, HttpResponseMessage> script)
        => _scripts.Enqueue(script);

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (_scripts.Count == 0)
        {
            throw new InvalidOperationException($"Unexpected request to '{request.RequestUri}'.");
        }

        return Task.FromResult(_scripts.Dequeue()(request));
    }
}

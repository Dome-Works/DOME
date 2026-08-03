using HomelabDocs.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HomelabDocs.Web.Components.Diagram;

public partial class CytoscapeDiagram : IAsyncDisposable
{
    private ElementReference _container;
    private IJSObjectReference? _module;
    private DotNetObjectReference<CytoscapeDiagram>? _dotNetRef;
    private bool _initialized;
    private bool _disposed;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public DiagramGraph Graph { get; set; } = new([], []);

    [Parameter]
    public EventCallback<string?> OnNodeSelected { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_initialized || _disposed)
        {
            return;
        }

        try
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            _module = await JS.InvokeAsync<IJSObjectReference>(
                "import",
                "./js/cytoscapeDiagram.js");

            await _module.InvokeVoidAsync(
                "initialize",
                _container,
                ToJsGraph(Graph),
                _dotNetRef);

            _initialized = true;
        }
        catch (InvalidOperationException)
        {
            // JS interop is unavailable during static prerender; retry after the circuit connects.
            _dotNetRef?.Dispose();
            _dotNetRef = null;
            _module = null;
        }
        catch (JSException)
        {
            _dotNetRef?.Dispose();
            _dotNetRef = null;

            if (_module is not null)
            {
                await _module.DisposeAsync();
                _module = null;
            }

            throw;
        }
    }

    [JSInvokable]
    public Task NotifyNodeSelectedAsync(string? nodeId)
    {
        return OnNodeSelected.InvokeAsync(nodeId);
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        try
        {
            if (_module is not null)
            {
                await _module.InvokeVoidAsync("dispose");
                await _module.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
            // Circuit already disconnected; nothing left to clean up on the client.
        }
        finally
        {
            _module = null;
            _dotNetRef?.Dispose();
            _dotNetRef = null;
        }
    }

    private static object ToJsGraph(DiagramGraph graph)
    {
        return new
        {
            nodes = graph.Nodes.Select(n => new
            {
                id = n.Id,
                label = n.Label,
                type = n.Type,
                status = n.Status
            }),
            edges = graph.Edges.Select(e => new
            {
                id = e.Id,
                source = e.SourceId,
                target = e.TargetId,
                label = e.Label
            })
        };
    }
}

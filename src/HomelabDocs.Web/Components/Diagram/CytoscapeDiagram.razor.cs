using HomelabDocs.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HomelabDocs.Web.Components.Diagram;

public partial class CytoscapeDiagram : IAsyncDisposable
{
    private ElementReference _container;
    private IJSObjectReference? _module;
    private bool _initialized;
    private bool _disposed;
    private DiagramGraph? _renderedGraph;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public DiagramGraph Graph { get; set; } = new([], []);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_disposed)
        {
            return;
        }

        if (!_initialized)
        {
            await InitializeAsync();
            return;
        }

        if (!ReferenceEquals(_renderedGraph, Graph))
        {
            await UpdateGraphAsync();
        }
    }

    private async Task InitializeAsync()
    {
        try
        {
            _module = await JS.InvokeAsync<IJSObjectReference>(
                "import",
                "./js/cytoscapeDiagram.js");

            await _module.InvokeVoidAsync(
                "initialize",
                _container,
                ToJsGraph(Graph));

            _initialized = true;
            _renderedGraph = Graph;
        }
        catch (InvalidOperationException)
        {
            // JS interop is unavailable during static prerender; retry after the circuit connects.
            _module = null;
        }
        catch (JSException)
        {
            if (_module is not null)
            {
                await _module.DisposeAsync();
                _module = null;
            }

            throw;
        }
    }

    private async Task UpdateGraphAsync()
    {
        if (_module is null || _disposed)
        {
            return;
        }

        try
        {
            await _module.InvokeVoidAsync("updateGraph", ToJsGraph(Graph));
            _renderedGraph = Graph;
        }
        catch (JSDisconnectedException)
        {
            // Circuit already disconnected.
        }
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

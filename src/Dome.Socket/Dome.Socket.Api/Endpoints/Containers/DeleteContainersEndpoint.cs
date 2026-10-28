using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Contracts.Containers;
using FastEndpoints;

namespace Dome.Socket.Api.Endpoints.Containers;

public sealed class DeleteContainersEndpoint : Endpoint<DeleteContainersRequest, DeleteContainersResponse>
{
    private const int MaxContainerIds = 100;

    private readonly IDockerEngineClient _dockerEngineClient;

    public DeleteContainersEndpoint(IDockerEngineClient dockerEngineClient)
    {
        _dockerEngineClient = dockerEngineClient;
    }

    public override void Configure()
    {
        Post("/api/containers/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteContainersRequest req, CancellationToken ct)
    {
        if (req.ContainerIds is not { Count: > 0 and <= MaxContainerIds } ||
            req.ContainerIds.Any(string.IsNullOrWhiteSpace))
        {
            AddError(r => r.ContainerIds, $"ContainerIds must contain between 1 and {MaxContainerIds} non-empty ids.");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var results = new List<DeleteContainerResult>();
        foreach (var id in req.ContainerIds.Select(i => i.Trim()).Distinct(StringComparer.Ordinal))
        {
            try
            {
                var result = await _dockerEngineClient.RemoveContainerAsync(id, req.RemoveVolumes, ct);
                var status = result == DockerContainerLifecycleResult.NotFound
                    ? DeleteContainerStatus.NotFound
                    : DeleteContainerStatus.Deleted;
                results.Add(new DeleteContainerResult { ContainerId = id, Status = status });
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                results.Add(new DeleteContainerResult { ContainerId = id, Status = DeleteContainerStatus.Failed, Error = ex.Message });
            }
        }

        await Send.OkAsync(new DeleteContainersResponse { Results = results }, ct);
    }
}

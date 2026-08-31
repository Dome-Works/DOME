namespace HomelabDocs.Shared.Sockets;

public sealed record GetSocketStatusesResponse
{
    public IReadOnlyCollection<SocketStatusResponse> Statuses { get; init; }
        = Array.Empty<SocketStatusResponse>();
}

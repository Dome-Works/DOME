using HomelabDocs.Shared.Sockets;

namespace HomelabDocs.Business.Sockets;

public sealed record SocketMutationResult
{
    public SocketResponse? Socket { get; init; }

    public string? Error { get; init; }

    public bool IsConflict { get; init; }

    public bool IsNotFound { get; init; }

    public bool IsSuccess => Socket is not null && Error is null && !IsNotFound;

    public static SocketMutationResult Success(SocketResponse socket)
        => new() { Socket = socket };

    public static SocketMutationResult Invalid(string error)
        => new() { Error = error };

    public static SocketMutationResult Conflict(string error)
        => new() { Error = error, IsConflict = true };

    public static SocketMutationResult NotFound()
        => new() { IsNotFound = true };
}

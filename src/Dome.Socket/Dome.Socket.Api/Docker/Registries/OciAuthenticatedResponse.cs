namespace Dome.Socket.Api.Docker.Registries;

internal sealed record OciAuthenticatedResponse
{
    public required HttpResponseMessage Response { get; init; }

    public string? Token { get; init; }
}

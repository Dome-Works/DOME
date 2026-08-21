namespace HomelabDocs.Socket.Api.Docker.Configuration;

internal static class DockerEndpointValidator
{
    public static Uri Validate(string? endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new InvalidOperationException(
                "Docker:Endpoint is required.");
        }

        if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var uri))
        {
            throw new InvalidOperationException(
                $"Docker:Endpoint '{endpoint}' is not a valid absolute URI.");
        }

        if (!string.Equals(uri.Scheme, "unix", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Docker:Endpoint must use the unix scheme. Remote Docker Engine URIs are not supported. Value: '{endpoint}'.");
        }

        return uri;
    }
}

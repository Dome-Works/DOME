namespace Dome.Business.Sockets;

public static class SocketAddressValidator
{
    public static bool TryValidate(string? address, out string error)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            error = "Address is required.";
            return false;
        }

        if (!Uri.TryCreate(address, UriKind.Absolute, out var uri))
        {
            error = "Address must be an absolute URI.";
            return false;
        }

        if (!string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase)
            && !string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
        {
            error = "Address must use http or https.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}

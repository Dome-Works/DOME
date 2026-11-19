using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;

namespace Dome.Business.Sockets;

public interface ISocketApiKeys
{
    string Protect(string key);

    string? Unprotect(string protectedKey);
}

public sealed class SocketApiKeys : ISocketApiKeys
{
    private readonly IDataProtector _protector;

    public SocketApiKeys(IDataProtectionProvider dataProtectionProvider)
    {
        _protector = dataProtectionProvider.CreateProtector("Dome.Sockets.ApiKey");
    }

    public string Protect(string key)
    {
        return _protector.Protect(key);
    }

    public string? Unprotect(string protectedKey)
    {
        if (string.IsNullOrEmpty(protectedKey))
        {
            return null;
        }

        try
        {
            return _protector.Unprotect(protectedKey);
        }
        catch (CryptographicException)
        {
            return null;
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace Dome.Socket.Api.Security;

public sealed class SocketKey
{
    public const string KeyFileName = "socket.key";
    internal const string PairedFileName = "paired";

    private readonly string _pairedPath;
    private readonly string _keyHash;
    private int _paired;

    private SocketKey(string value, bool isConfigured, string directory)
    {
        Value = value;
        IsConfigured = isConfigured;
        KeyPath = Path.Combine(directory, KeyFileName);
        _pairedPath = Path.Combine(directory, PairedFileName);
        _keyHash = Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
        _paired = File.Exists(_pairedPath) && File.ReadAllText(_pairedPath).Trim() == _keyHash ? 1 : 0;
    }

    public string Value { get; }

    public bool IsConfigured { get; }

    public string KeyPath { get; }

    public bool IsPaired => Volatile.Read(ref _paired) == 1;

    public static SocketKey LoadOrCreate(string directory, string? configuredKey)
    {
        Directory.CreateDirectory(directory);

        if (!string.IsNullOrWhiteSpace(configuredKey))
        {
            return new SocketKey(configuredKey.Trim(), true, directory);
        }

        var path = Path.Combine(directory, KeyFileName);
        var exists = File.Exists(path);
        var stored = exists ? File.ReadAllText(path).Trim() : string.Empty;
        if (exists)
        {
            PrivateFile.Restrict(path);
        }

        if (stored.Length == 0)
        {
            stored = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
            PrivateFile.Write(path, Encoding.UTF8.GetBytes(stored));
        }

        return new SocketKey(stored, false, directory);
    }

    public bool MarkPaired()
    {
        if (Interlocked.Exchange(ref _paired, 1) == 1)
        {
            return false;
        }

        File.WriteAllText(_pairedPath, _keyHash);
        return true;
    }
}

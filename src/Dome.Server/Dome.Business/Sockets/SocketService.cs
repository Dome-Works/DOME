using System.Net;
using Dome.Domain.Sockets;
using Dome.Shared.Sockets;
using Microsoft.Extensions.Logging;
using Refit;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Sockets;

public sealed class SocketService : ISocketService
{
    private readonly ISocketRepository _socketRepository;
    private readonly IDomeSocketApiFactory _socketApiFactory;
    private readonly IApplicationVersion _applicationVersion;
    private readonly ISocketApiKeys _apiKeys;
    private readonly ILogger<SocketService> _logger;

    public SocketService(
        ISocketRepository socketRepository,
        IDomeSocketApiFactory socketApiFactory,
        IApplicationVersion applicationVersion,
        ISocketApiKeys apiKeys,
        ILogger<SocketService> logger)
    {
        _apiKeys = apiKeys;
        _socketRepository = socketRepository;
        _socketApiFactory = socketApiFactory;
        _applicationVersion = applicationVersion;
        _logger = logger;
    }

    public async Task<IReadOnlyList<SocketResponse>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        var sockets = await _socketRepository.ListAsync(cancellationToken);
        return sockets.Select(Map).ToArray();
    }

    public async Task<SocketResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        return socket is null ? null : Map(socket);
    }

    public async Task<SocketMutationResult> CreateAsync(
        string name,
        string address,
        CancellationToken cancellationToken = default)
    {
        var validationError = Validate(name, address);
        if (validationError is not null)
        {
            return SocketMutationResult.Invalid(validationError);
        }

        var trimmedName = name.Trim();
        var trimmedAddress = address.Trim();

        if (await _socketRepository.NameExistsAsync(trimmedName, excludeId: null, cancellationToken))
        {
            return SocketMutationResult.Conflict(
                $"A socket named '{trimmedName}' already exists.");
        }

        var apiKey = _apiKeys.Generate();
        var socket = new SocketEntity
        {
            Id = Guid.NewGuid(),
            Name = trimmedName,
            Address = trimmedAddress,
            ProtectedApiKey = _apiKeys.Protect(apiKey),
            CertificateFingerprint = null,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _socketRepository.AddAsync(socket, cancellationToken);
        return SocketMutationResult.Success(Map(socket), apiKey);
    }

    public async Task<SocketMutationResult> RotateKeyAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return SocketMutationResult.NotFound();
        }

        var apiKey = _apiKeys.Generate();
        socket.ProtectedApiKey = _apiKeys.Protect(apiKey);
        socket.CertificateFingerprint = null;
        await _socketRepository.UpdateAsync(socket, cancellationToken);
        return SocketMutationResult.Success(Map(socket), apiKey);
    }

    public async Task<SocketMutationResult> UpdateAsync(
        Guid id,
        string name,
        string address,
        CancellationToken cancellationToken = default)
    {
        var validationError = Validate(name, address);
        if (validationError is not null)
        {
            return SocketMutationResult.Invalid(validationError);
        }

        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return SocketMutationResult.NotFound();
        }

        var trimmedName = name.Trim();
        var trimmedAddress = address.Trim();

        if (await _socketRepository.NameExistsAsync(trimmedName, id, cancellationToken))
        {
            return SocketMutationResult.Conflict(
                $"A socket named '{trimmedName}' already exists.");
        }

        if (!string.Equals(socket.Address, trimmedAddress, StringComparison.Ordinal))
        {
            socket.CertificateFingerprint = null;
        }

        socket.Name = trimmedName;
        socket.Address = trimmedAddress;
        await _socketRepository.UpdateAsync(socket, cancellationToken);
        return SocketMutationResult.Success(Map(socket));
    }

    public async Task<IReadOnlyList<SocketStatusResponse>> GetStatusesAsync(
        CancellationToken cancellationToken = default)
    {
        var sockets = await _socketRepository.ListAsync(cancellationToken);
        var unpinned = sockets.Where(socket => socket.CertificateFingerprint is null).ToArray();
        var statuses = await Task.WhenAll(
            sockets.Select(socket => GetStatusAsync(socket, cancellationToken)));

        foreach (var socket in unpinned.Where(socket => socket.CertificateFingerprint is not null))
        {
            await _socketRepository.UpdateAsync(socket, cancellationToken);
        }

        return statuses;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return false;
        }

        await _socketRepository.DeleteAsync(socket, cancellationToken);
        return true;
    }

    private static string? Validate(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Name is required.";
        }

        return SocketAddressValidator.TryValidate(address, out var error)
            ? null
            : error;
    }

    private static SocketResponse Map(SocketEntity socket)
        => new()
        {
            Id = socket.Id,
            Name = socket.Name,
            Address = socket.Address,
            CreatedAt = socket.CreatedAt
        };

    private async Task<SocketStatusResponse> GetStatusAsync(
        SocketEntity socket,
        CancellationToken cancellationToken)
    {
        try
        {
            if (socket.CertificateFingerprint is null)
            {
                var fingerprint = await _socketApiFactory.PairAsync(socket, cancellationToken);
                if (fingerprint is null)
                {
                    return UnavailableStatus(socket, SocketState.NeedsPairing);
                }

                socket.CertificateFingerprint = fingerprint;
            }

            var api = _socketApiFactory.Create(socket);
            var health = await api.GetHealthAsync(cancellationToken);
            return new SocketStatusResponse
            {
                Id = socket.Id,
                IsReachable = true,
                State = SocketState.Online,
                Version = health.Version,
                NeedsUpdate = NeedsUpdate(health.Version)
            };
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning(
                "Socket '{SocketName}' at '{Address}' rejected the API key.",
                socket.Name,
                socket.Address);

            return UnavailableStatus(socket, SocketState.Unauthorized);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(
                ex,
                "Failed to reach socket '{SocketName}' at '{Address}'.",
                socket.Name,
                socket.Address);

            return UnavailableStatus(socket, SocketState.Offline);
        }
    }

    private static SocketStatusResponse UnavailableStatus(SocketEntity socket, SocketState state)
        => new()
        {
            Id = socket.Id,
            IsReachable = false,
            State = state,
            NeedsUpdate = false
        };

    private bool NeedsUpdate(string? socketVersion)
        => !string.Equals(
            Normalize(_applicationVersion.Current),
            Normalize(socketVersion),
            StringComparison.OrdinalIgnoreCase);

    private static string? Normalize(string? version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            return null;
        }

        var trimmed = version.Trim();
        var plus = trimmed.IndexOf('+');
        if (plus >= 0)
        {
            trimmed = trimmed[..plus];
        }

        if (trimmed.StartsWith('v') || trimmed.StartsWith('V'))
        {
            trimmed = trimmed[1..];
        }

        return trimmed.Length == 0 ? null : trimmed;
    }
}

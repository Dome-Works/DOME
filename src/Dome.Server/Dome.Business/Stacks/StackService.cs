using System.Text;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Shared.Stacks;
using SocketEntity = Dome.Domain.Sockets.Socket;
using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Stacks;

public sealed class StackService : IStackService
{
    internal const string DefaultComposeYaml = "services: {}";
    internal const int MaxComposeYamlBytes = 512 * 1024;

    private readonly ISocketRepository _socketRepository;
    private readonly IStackRepository _stackRepository;

    public StackService(
        ISocketRepository socketRepository,
        IStackRepository stackRepository)
    {
        _socketRepository = socketRepository;
        _stackRepository = stackRepository;
    }

    public async Task<IReadOnlyList<StackDto>?> ListAsync(
        string socketName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? null
            : await ListForSocketAsync(socket.Id, cancellationToken);
    }

    public async Task<StackDto?> GetAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? null
            : await GetForSocketAsync(socket.Id, stackId, cancellationToken);
    }

    public async Task<StackMutationResult> CreateAsync(
        string socketName,
        string projectName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? StackMutationResult.NotFound()
            : await CreateForSocketAsync(socket, projectName, cancellationToken);
    }

    public async Task<StackMutationResult> UpdateAsync(
        string socketName,
        Guid stackId,
        string projectName,
        string composeYaml,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? StackMutationResult.NotFound()
            : await UpdateForSocketAsync(socket.Id, stackId, projectName, composeYaml, cancellationToken);
    }

    public async Task<bool?> DeleteAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? null
            : await DeleteForSocketAsync(socket.Id, stackId, cancellationToken);
    }

    private async Task<IReadOnlyList<StackDto>> ListForSocketAsync(
        Guid socketId,
        CancellationToken cancellationToken)
    {
        var stacks = await _stackRepository.ListBySocketIdAsync(socketId, cancellationToken);
        return stacks.Select(Map).ToArray();
    }

    private async Task<StackDto?> GetForSocketAsync(
        Guid socketId,
        Guid stackId,
        CancellationToken cancellationToken)
    {
        var stack = await _stackRepository.GetByIdAsync(stackId, cancellationToken);
        if (stack is null || stack.SocketId != socketId)
        {
            return null;
        }

        return Map(stack);
    }

    private async Task<StackMutationResult> CreateForSocketAsync(
        SocketEntity socket,
        string projectName,
        CancellationToken cancellationToken)
    {
        if (!StackProjectName.TryValidate(projectName, out var normalizedProjectName, out var nameError))
        {
            return StackMutationResult.Invalid(nameError!);
        }

        if (await _stackRepository.ProjectNameExistsAsync(
            socket.Id,
            normalizedProjectName,
            excludeId: null,
            cancellationToken))
        {
            return StackMutationResult.Conflict(
                $"A stack named '{normalizedProjectName}' already exists on this device.");
        }

        var now = DateTimeOffset.UtcNow;
        var stack = new StackEntity
        {
            Id = Guid.NewGuid(),
            SocketId = socket.Id,
            ProjectName = normalizedProjectName,
            ComposeYaml = DefaultComposeYaml,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _stackRepository.AddAsync(stack, cancellationToken);
        return StackMutationResult.Success(Map(stack));
    }

    private async Task<StackMutationResult> UpdateForSocketAsync(
        Guid socketId,
        Guid stackId,
        string projectName,
        string composeYaml,
        CancellationToken cancellationToken)
    {
        var stack = await _stackRepository.GetByIdAsync(stackId, cancellationToken);
        if (stack is null || stack.SocketId != socketId)
        {
            return StackMutationResult.NotFound();
        }

        if (!StackProjectName.TryValidate(projectName, out var normalizedProjectName, out var nameError))
        {
            return StackMutationResult.Invalid(nameError!);
        }

        var yamlError = ValidateComposeYaml(composeYaml, out var storedYaml);
        if (yamlError is not null)
        {
            return StackMutationResult.Invalid(yamlError);
        }

        if (await _stackRepository.ProjectNameExistsAsync(
            socketId,
            normalizedProjectName,
            stack.Id,
            cancellationToken))
        {
            return StackMutationResult.Conflict(
                $"A stack named '{normalizedProjectName}' already exists on this device.");
        }

        stack.ProjectName = normalizedProjectName;
        stack.ComposeYaml = storedYaml;
        stack.UpdatedAt = DateTimeOffset.UtcNow;
        await _stackRepository.UpdateAsync(stack, cancellationToken);
        return StackMutationResult.Success(Map(stack));
    }

    private async Task<bool> DeleteForSocketAsync(
        Guid socketId,
        Guid stackId,
        CancellationToken cancellationToken)
    {
        var stack = await _stackRepository.GetByIdAsync(stackId, cancellationToken);
        if (stack is null || stack.SocketId != socketId)
        {
            return false;
        }

        await _stackRepository.DeleteAsync(stack, cancellationToken);
        return true;
    }

    private static string? ValidateComposeYaml(string? composeYaml, out string storedYaml)
    {
        storedYaml = string.Empty;
        if (string.IsNullOrWhiteSpace(composeYaml))
        {
            return "Compose file is required.";
        }

        var byteCount = Encoding.UTF8.GetByteCount(composeYaml);
        if (byteCount > MaxComposeYamlBytes)
        {
            return "Compose file must be 512 KiB or smaller.";
        }

        storedYaml = composeYaml;
        return null;
    }

    private static StackDto Map(StackEntity stack)
        => new()
        {
            Id = stack.Id,
            SocketId = stack.SocketId,
            ProjectName = stack.ProjectName,
            ComposeYaml = stack.ComposeYaml,
            CreatedAt = stack.CreatedAt,
            UpdatedAt = stack.UpdatedAt
        };
}

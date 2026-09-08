using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Shared.Stacks;
using SocketEntity = Dome.Domain.Sockets.Socket;
using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Stacks;

public sealed class StackService : IStackService
{
    internal const string DefaultComposeYaml = "services: {}";

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
        string composeName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? StackMutationResult.NotFound()
            : await CreateForSocketAsync(socket, composeName, cancellationToken);
    }

    public async Task<StackMutationResult> UpdateAsync(
        string socketName,
        Guid stackId,
        string composeName,
        string composeYaml,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        return socket is null
            ? StackMutationResult.NotFound()
            : await UpdateForSocketAsync(socket.Id, stackId, composeName, composeYaml, cancellationToken);
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
        string composeName,
        CancellationToken cancellationToken)
    {
        var validationError = ValidateComposeName(composeName, out var trimmedComposeName, out var projectName);
        if (validationError is not null)
        {
            return StackMutationResult.Invalid(validationError);
        }

        if (await _stackRepository.ProjectNameExistsAsync(
            socket.Id,
            projectName,
            excludeId: null,
            cancellationToken))
        {
            return StackMutationResult.Conflict(
                $"A stack named '{projectName}' already exists on this device.");
        }

        var now = DateTimeOffset.UtcNow;
        var stack = new StackEntity
        {
            Id = Guid.NewGuid(),
            SocketId = socket.Id,
            ComposeName = trimmedComposeName,
            ProjectName = projectName,
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
        string composeName,
        string composeYaml,
        CancellationToken cancellationToken)
    {
        var stack = await _stackRepository.GetByIdAsync(stackId, cancellationToken);
        if (stack is null || stack.SocketId != socketId)
        {
            return StackMutationResult.NotFound();
        }

        var composeNameError = ValidateComposeName(
            composeName,
            out var trimmedComposeName,
            out var projectName);
        if (composeNameError is not null)
        {
            return StackMutationResult.Invalid(composeNameError);
        }

        var yamlError = ValidateComposeYaml(composeYaml, out var trimmedYaml);
        if (yamlError is not null)
        {
            return StackMutationResult.Invalid(yamlError);
        }

        if (await _stackRepository.ProjectNameExistsAsync(
            socketId,
            projectName,
            stack.Id,
            cancellationToken))
        {
            return StackMutationResult.Conflict(
                $"A stack named '{projectName}' already exists on this device.");
        }

        stack.ComposeName = trimmedComposeName;
        stack.ProjectName = projectName;
        stack.ComposeYaml = trimmedYaml;
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

    private static string? ValidateComposeName(
        string composeName,
        out string trimmedComposeName,
        out string projectName)
    {
        trimmedComposeName = composeName.Trim();
        projectName = string.Empty;

        if (trimmedComposeName.Length == 0)
        {
            return "Compose name is required.";
        }

        if (trimmedComposeName.Length > 128)
        {
            return "Compose name must be 128 characters or fewer.";
        }

        projectName = StackProjectName.FromDisplayName(trimmedComposeName);
        if (projectName.Length == 0)
        {
            return "Compose name must contain at least one letter, number, hyphen, or underscore.";
        }

        return null;
    }

    private static string? ValidateComposeYaml(string composeYaml, out string trimmedYaml)
    {
        trimmedYaml = composeYaml.Trim();
        if (trimmedYaml.Length == 0)
        {
            return "Compose file is required.";
        }

        return null;
    }

    private static StackDto Map(StackEntity stack)
        => new()
        {
            Id = stack.Id,
            SocketId = stack.SocketId,
            ComposeName = stack.ComposeName,
            ProjectName = stack.ProjectName,
            ComposeYaml = stack.ComposeYaml,
            CreatedAt = stack.CreatedAt,
            UpdatedAt = stack.UpdatedAt
        };
}

using Dome.Shared.Stacks;

namespace Dome.Business.Stacks;

public sealed record StackMutationResult
{
    public StackDto? Stack { get; init; }

    public string? Error { get; init; }

    public bool IsConflict { get; init; }

    public bool IsNotFound { get; init; }

    public bool IsSuccess => Stack is not null && Error is null && !IsNotFound;

    public static StackMutationResult Success(StackDto stack)
        => new() { Stack = stack };

    public static StackMutationResult Invalid(string error)
        => new() { Error = error };

    public static StackMutationResult Conflict(string error)
        => new() { Error = error, IsConflict = true };

    public static StackMutationResult NotFound()
        => new() { IsNotFound = true };
}

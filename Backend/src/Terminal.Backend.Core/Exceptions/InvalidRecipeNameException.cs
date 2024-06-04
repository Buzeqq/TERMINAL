namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidRecipeNameException(string? name)
    : TerminalException($"Unable to create recipe name with: [{name}]");
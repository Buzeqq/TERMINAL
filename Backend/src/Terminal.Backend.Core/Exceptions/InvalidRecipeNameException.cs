namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidRecipeNameException : TerminalException
{
    public InvalidRecipeNameException(string? name) : base($"Unable to create recipe name with: [{name}]")
    {
    }
}
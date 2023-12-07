namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidEntityIdException : TerminalException
{
    public InvalidEntityIdException(object id) : base($"Unable to set {id} as entity identifier.")
    {
    }
}
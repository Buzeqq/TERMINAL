namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidEntityIdException(object id) : TerminalException($"Unable to set {id} as entity identifier.");

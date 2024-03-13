namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidEmailException(string value) : TerminalException(
    "Invalid email",
    $"Given email: {value} is invalid");

public sealed class EmailAlreadyExistsException(string email) : TerminalException(
    "Email already exists",
    $"User with email {email} already exists, try to login with it");
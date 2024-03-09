namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidSampleCodeException(string code)
    : TerminalException($"Unable to create sample code with: [{code}]");
namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidSampleCodeException : TerminalException
{
    public InvalidSampleCodeException(string code) : base($"Unable to create sample code with: [{code}]")
    {
    }
}
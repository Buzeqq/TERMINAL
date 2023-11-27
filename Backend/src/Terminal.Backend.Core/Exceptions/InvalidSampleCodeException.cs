namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidSampleCodeException : TerminalException
{
    public InvalidSampleCodeException(string code) : base($"Unable to create measurement code with: [{code}]") 
    { }
}
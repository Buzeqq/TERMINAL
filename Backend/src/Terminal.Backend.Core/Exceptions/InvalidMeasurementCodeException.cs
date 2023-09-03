namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidMeasurementCodeException : TerminalException
{
    public InvalidMeasurementCodeException(string code) : base($"Unable to create measurement code with: [{code}]") 
    { }
}
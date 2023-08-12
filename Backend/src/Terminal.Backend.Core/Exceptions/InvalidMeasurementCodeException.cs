namespace Terminal.Backend.Core.Exceptions;

public class InvalidMeasurementCodeException : TerminalException
{
    public InvalidMeasurementCodeException(string code) : base($"Unable to create measurement code with: [{code}]") 
    { }
}
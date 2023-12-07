using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public class SampleNotFoundException : TerminalException
{
    public SampleNotFoundException() : base("Sample not found!")
    {
    }
}
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class UnknownParameterTypeException : TerminalException
{
    public UnknownParameterTypeException(Parameter parameter) : 
        base($"Unknown type of base type {typeof(Parameter).FullName}")
    {
    }
}
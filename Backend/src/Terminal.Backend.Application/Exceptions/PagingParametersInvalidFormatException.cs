using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class PagingParametersInvalidFormatException : TerminalException
{
    public PagingParametersInvalidFormatException(string message) : base(message)
    {
    }
}
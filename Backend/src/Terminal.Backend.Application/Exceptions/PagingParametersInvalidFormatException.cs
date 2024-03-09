using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class PagingParametersInvalidFormatException(string message) : TerminalException(message);
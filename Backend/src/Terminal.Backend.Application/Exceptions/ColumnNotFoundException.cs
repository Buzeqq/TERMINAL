using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public class ColumnNotFoundException : TerminalException
{
    public ColumnNotFoundException(string columnName) : base($"Column not found {columnName}")
    {
    }
}
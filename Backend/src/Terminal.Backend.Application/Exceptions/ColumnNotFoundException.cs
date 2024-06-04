using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public class ColumnNotFoundException(string columnName) : TerminalException($"Column not found {columnName}");
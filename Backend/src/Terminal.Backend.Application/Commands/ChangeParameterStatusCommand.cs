using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Commands;

public sealed record ChangeParameterStatusCommand(string Name, bool Status) : ICommand;
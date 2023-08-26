using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Commands.Parameters;

public abstract record CreateParameterCommand(string Name) : ICommand;
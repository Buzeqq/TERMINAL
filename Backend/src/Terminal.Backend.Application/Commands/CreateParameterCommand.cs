using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.Commands;

public sealed record CreateParameterCommand(Parameter Parameter) : ICommand;
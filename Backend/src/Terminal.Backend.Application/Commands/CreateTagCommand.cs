using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Commands;

public sealed record CreateTagCommand(string Name) : ICommand;
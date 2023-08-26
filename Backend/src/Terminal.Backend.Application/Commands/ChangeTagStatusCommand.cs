using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands;

public sealed record ChangeTagStatusCommand(TagName Name, bool IsActive) : ICommand;
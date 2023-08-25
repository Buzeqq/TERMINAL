using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands;

public sealed record ChangeTagStatusCommand(
    [property: System.Text.Json.Serialization.JsonIgnore] TagName Name,
    bool IsActive) : ICommand;
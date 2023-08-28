using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Commands;

public sealed record ChangeProjectStatusCommand(
    [property: System.Text.Json.Serialization.JsonIgnore] Guid ProjectId
    , bool IsActive) : ICommand;
using MediatR;

namespace Terminal.Backend.Application.Commands.Project.ChangeStatus;

public sealed record ChangeProjectStatusCommand(
    [property: System.Text.Json.Serialization.JsonIgnore] Guid ProjectId
    , bool IsActive) : IRequest;
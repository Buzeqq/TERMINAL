using MediatR;

namespace Terminal.Backend.Application.Commands.Project.Create;

public sealed record CreateProjectCommand(
    [property: System.Text.Json.Serialization.JsonIgnore] Guid Id, 
    string Name) : IRequest;
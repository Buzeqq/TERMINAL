using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Projects.Create;

public record CreateProjectCommand(ProjectId Id, string Name) : IRequest;

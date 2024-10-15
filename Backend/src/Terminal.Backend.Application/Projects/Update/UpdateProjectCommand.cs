using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Projects.Update;

public record UpdateProjectCommand(ProjectId Id, ProjectName? Name) : IRequest;

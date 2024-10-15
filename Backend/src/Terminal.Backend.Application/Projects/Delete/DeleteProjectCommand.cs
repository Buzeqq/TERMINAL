using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Projects.Delete;

public record DeleteProjectCommand(ProjectId Id) : IRequest;

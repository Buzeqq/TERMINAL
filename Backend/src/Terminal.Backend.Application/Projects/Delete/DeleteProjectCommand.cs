namespace Terminal.Backend.Application.Projects.Delete;

public sealed record DeleteProjectCommand(Guid Id) : IRequest;
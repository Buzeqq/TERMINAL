using MediatR;

namespace Terminal.Backend.Application.Commands.Project.Delete;

public sealed record DeleteProjectCommand(Guid Id) : IRequest;
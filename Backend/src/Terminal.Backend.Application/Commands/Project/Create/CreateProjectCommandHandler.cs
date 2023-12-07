using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Project.Create;

internal sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task Handle(CreateProjectCommand request, CancellationToken ct)
    {
        var newProject = new Core.Entities.Project(request.Id, request.Name);
        return _projectRepository.AddAsync(newProject, ct);
    }
}
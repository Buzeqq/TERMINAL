using MediatR;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Project.Create;

internal sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(CreateProjectCommand request, CancellationToken ct)
    {
        var newProjectId = ProjectId.Create();
        var newProject = new Core.Entities.Project(newProjectId, request.Name);
        await _projectRepository.AddAsync(newProject, ct);
    }
}
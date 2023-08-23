using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Handlers;

public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task HandleAsync(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var newProjectId = ProjectId.Create();
        var newProject = new Project(newProjectId, request.ProjectName);
        await _projectRepository.AddAsync(newProject, cancellationToken);
    }
}
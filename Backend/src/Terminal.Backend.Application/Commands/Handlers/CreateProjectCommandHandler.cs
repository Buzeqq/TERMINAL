using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Handlers;

internal sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task HandleAsync(CreateProjectCommand request, CancellationToken ct)
    {
        var newProjectId = ProjectId.Create();
        var newProject = new Project(newProjectId, request.Name);
        await _projectRepository.AddAsync(newProject, ct);
    }
}
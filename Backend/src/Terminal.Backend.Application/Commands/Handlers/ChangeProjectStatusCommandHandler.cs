using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

public sealed class ChangeProjectStatusCommandHandler : ICommandHandler<ChangeProjectStatusCommand>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectStatusCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task HandleAsync(ChangeProjectStatusCommand command, CancellationToken cancellationToken)
    {
        var (projectId, isActive) = command;
        var project = await _projectRepository.GetAsync(projectId, cancellationToken);
        if (project is null || project.IsActive == isActive)
        {
            return;
        }

        project.ChangeProjectStatus(isActive);
        await _projectRepository.UpdateAsync(project, cancellationToken);
    }
}
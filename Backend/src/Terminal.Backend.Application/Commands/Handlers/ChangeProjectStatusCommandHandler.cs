using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

public sealed class ChangeProjectStatusCommandHandler : ICommandHandler<ChangeProjectStatusCommand>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectStatusCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task HandleAsync(ChangeProjectStatusCommand command, CancellationToken ct)
    {
        var (projectId, isActive) = command;
        var project = await _projectRepository.GetAsync(projectId, ct);
        if (project is null)
        {
            throw new ProjectNotFoundException(projectId);
        }

        project.ChangeStatus(isActive);
        await _projectRepository.UpdateAsync(project, ct);
    }
}
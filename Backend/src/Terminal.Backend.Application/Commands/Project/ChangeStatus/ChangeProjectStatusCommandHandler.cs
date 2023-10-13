using MediatR;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Project.ChangeStatus;

internal sealed class ChangeProjectStatusCommandHandler : IRequestHandler<ChangeProjectStatusCommand>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectStatusCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(ChangeProjectStatusCommand command, CancellationToken ct)
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
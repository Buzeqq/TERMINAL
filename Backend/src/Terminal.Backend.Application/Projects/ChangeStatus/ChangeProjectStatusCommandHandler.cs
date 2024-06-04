using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Projects.ChangeStatus;

internal sealed class ChangeProjectStatusCommandHandler(IProjectRepository projectRepository)
    : IRequestHandler<ChangeProjectStatusCommand>
{
    public async Task Handle(ChangeProjectStatusCommand command, CancellationToken ct)
    {
        var (projectId, isActive) = command;
        var project = await projectRepository.GetAsync(projectId, ct);
        if (project is null)
        {
            throw new ProjectNotFoundException();
        }

        project.ChangeStatus(isActive);
        await projectRepository.UpdateAsync(project, ct);
    }
}
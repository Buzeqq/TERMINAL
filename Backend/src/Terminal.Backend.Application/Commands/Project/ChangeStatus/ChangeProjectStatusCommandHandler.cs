using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

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
            throw new ProjectNotFoundException();
        }

        project.ChangeStatus(isActive);
        await _projectRepository.UpdateAsync(project, ct);
    }
}
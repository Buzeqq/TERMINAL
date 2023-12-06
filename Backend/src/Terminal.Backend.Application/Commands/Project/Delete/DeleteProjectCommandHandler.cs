using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Commands.Project.Delete;

internal sealed class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var project = await _projectRepository.GetAsync(id, cancellationToken);
        if (project is null)
        {
            throw new ProjectNotFoundException();
        }

        await _projectRepository.DeleteAsync(project, cancellationToken);
    }
}
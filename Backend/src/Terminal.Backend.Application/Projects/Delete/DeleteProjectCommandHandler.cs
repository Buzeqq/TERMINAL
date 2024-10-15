using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Projects.Delete;

internal sealed class DeleteProjectCommandHandler(IProjectRepository projectRepository)
    : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var project = await projectRepository.GetAsync(id, cancellationToken);
        if (project is null)
        {
            throw new ProjectNotFoundException();
        }

        await projectRepository.DeleteAsync(project, cancellationToken);
    }
}

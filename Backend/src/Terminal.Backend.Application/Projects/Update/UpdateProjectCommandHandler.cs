using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Projects.Update;

internal sealed class UpdateProjectCommandHandler(IProjectRepository projectRepository)
    : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var project = await projectRepository.GetAsync(id, cancellationToken);
        if (project is null)
        {
            throw new ProjectNotFoundException();
        }

        if (project.Name != request.Name && !await projectRepository.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            throw new InvalidProjectNameException(request.Name);
        }

        project.Update(request.Name);

        await projectRepository.UpdateAsync(project, cancellationToken);
    }
}

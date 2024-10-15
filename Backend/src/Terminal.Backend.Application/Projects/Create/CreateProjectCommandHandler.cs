using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Projects.Create;

internal sealed class CreateProjectCommandHandler(IProjectRepository projectRepository)
    : IRequestHandler<CreateProjectCommand>
{
    public Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var newProject = new Core.Entities.Project(request.Id, request.Name);
        return projectRepository.AddAsync(newProject, cancellationToken);
    }
}

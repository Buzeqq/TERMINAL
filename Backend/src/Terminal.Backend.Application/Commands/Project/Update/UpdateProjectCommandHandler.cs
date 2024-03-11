using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Commands.Project.Update;

internal sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var project = await _projectRepository.GetAsync(id, cancellationToken);
        if (project is null)
        {
            throw new ProjectNotFoundException();
        }

        if (project.Name == request.Name)
        {
            return;
        }
        
        if (!await _projectRepository.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            throw new InvalidProjectNameException(request.Name);
        }

        project.Update(request.Name);

        await _projectRepository.UpdateAsync(project, cancellationToken);
    }
}
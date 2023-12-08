using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Sample.Update;

internal sealed class UpdateSampleCommandHandler : IRequestHandler<UpdateSampleCommand>
{
    private readonly ISampleRepository _sampleRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IConvertDtoService _convertDtoService;

    public UpdateSampleCommandHandler(ISampleRepository sampleRepository,
        IProjectRepository projectRepository,
        IRecipeRepository recipeRepository,
        IConvertDtoService convertDtoService)
    {
        _sampleRepository = sampleRepository;
        _projectRepository = projectRepository;
        _recipeRepository = recipeRepository;
        _convertDtoService = convertDtoService;
    }

    public async Task Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
    {
        var (id, projectId, recipeId, stepsDto, tagIds, comment)
            = request;
        var sample = await _sampleRepository.GetAsync(id, cancellationToken);
        if (sample is null)
        {
            throw new SampleNotFoundException();
        }

        var project = await _projectRepository.GetAsync(projectId, cancellationToken);
        if (project is null)
        {
            throw new ProjectNotFoundException();
        }

        if (!project.IsActive)
        {
            throw new ProjectNotActiveException(project.Name);
        }

        Core.Entities.Recipe? recipe = null;
        if (recipeId is not null)
        {
            recipe = await _recipeRepository.GetAsync(recipeId, cancellationToken);
            if (recipe is null)
            {
                throw new RecipeNotFoundException();
            }
        }

        var steps = await _convertDtoService.ConvertAsync(stepsDto, cancellationToken);
        var tags = await _convertDtoService.ConvertAsync(tagIds.Select(t => (TagId)t), cancellationToken);

        sample.Update(project, recipe, steps, tags, comment);
        await _sampleRepository.UpdateAsync(sample, cancellationToken);
    }
}
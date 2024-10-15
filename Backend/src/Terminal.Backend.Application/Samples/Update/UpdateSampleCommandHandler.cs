using Terminal.Backend.Application.Common.Services;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Update;

internal sealed class UpdateSampleCommandHandler(
    ISampleRepository sampleRepository,
    IProjectRepository projectRepository,
    IRecipeRepository recipeRepository,
    IConvertDtoService convertDtoService)
    : IRequestHandler<UpdateSampleCommand>
{
    public async Task Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
    {
        var (id, projectId, recipeId, stepsDto, tagIds, comment)
            = request;
        var sample = await sampleRepository.GetAsync(id, cancellationToken);
        if (sample is null)
        {
            throw new SampleNotFoundException();
        }

        var project = await projectRepository.GetAsync(projectId, cancellationToken);
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
            recipe = await recipeRepository.GetAsync(recipeId, cancellationToken);
            if (recipe is null)
            {
                throw new RecipeNotFoundException();
            }
        }

        var steps = await convertDtoService.ConvertAsync(stepsDto, cancellationToken);
        var tags = await convertDtoService.ConvertAsync(tagIds.Select(t => (TagId)t), cancellationToken);

        sample.Update(project, recipe, steps, tags, comment);
        await sampleRepository.UpdateAsync(sample, cancellationToken);
    }
}

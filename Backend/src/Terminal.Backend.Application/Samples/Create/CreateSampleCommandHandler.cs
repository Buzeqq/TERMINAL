using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;
using ParameterValue = Terminal.Backend.Core.Entities.ParameterValues.ParameterValue;

namespace Terminal.Backend.Application.Samples.Create;

internal sealed class CreateSampleCommandHandler(
    IConvertDtoService convertService,
    IRecipeRepository recipeRepository,
    ISampleRepository sampleRepository,
    IProjectRepository projectRepository)
    : IRequestHandler<CreateSampleCommand>
{
    public async Task Handle(CreateSampleCommand command, CancellationToken ct)
    {
        var (sampleId, projectId, recipeId, stepsDto, tagsDto,
            comment, saveAsRecipe, recipeName) = command;

        var steps = (await convertService.ConvertAsync(stepsDto, ct)).ToList();
        Recipe? recipe = null;
        if (saveAsRecipe)
        {
            if (recipeName is null)
            {
                throw new InvalidRecipeNameException(recipeName);
            }

            // for new recipe we need to copy every step, and every parameter value in steps
            recipe = new Recipe(RecipeId.Create(), recipeName);
            foreach (var step in steps)
            {
                var parameters = new List<ParameterValue>(step.Parameters
                    .Select(p => p.DeepCopy(Guid.NewGuid())));
                recipe.Steps.Add(new RecipeStep(Guid.NewGuid(), step.Comment, parameters, recipe));
            }

            await recipeRepository.AddAsync(recipe, ct);
        }
        else if (recipeId is not null)
        {
            recipe = await recipeRepository.GetAsync(recipeId, ct);
        }

        var tags = await convertService.ConvertAsync(tagsDto.Select(t => new TagId(t)), ct);
        var project = await projectRepository.GetAsync(projectId, ct) ?? throw new ProjectNotFoundException();
        if (!project.IsActive)
        {
            throw new ProjectNotActiveException(project.Name);
        }

        var sample = new Sample(sampleId,
            project,
            recipe,
            new Comment(comment),
            steps.ToList(),
            tags.ToList());
        project.Samples.Add(sample);
        await sampleRepository.AddAsync(sample, ct);
    }
}
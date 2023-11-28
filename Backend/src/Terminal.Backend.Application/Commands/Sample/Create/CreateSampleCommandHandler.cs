using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Sample.Create;

internal sealed class CreateSampleCommandHandler : IRequestHandler<CreateSampleCommand>
{
    private readonly IConvertDtoService _convertService;
    private readonly IRecipeRepository _recipeRepository;
    private readonly ISampleRepository _sampleRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateSampleCommandHandler(IConvertDtoService convertService,
        IRecipeRepository recipeRepository, ISampleRepository sampleRepository, IProjectRepository projectRepository)
    {
        _convertService = convertService;
        _recipeRepository = recipeRepository;
        _sampleRepository = sampleRepository;
        _projectRepository = projectRepository;
    }

    public async Task Handle(CreateSampleCommand command, CancellationToken ct)
    {
        var (sampleId, projectId, recipeId, stepsDto, tagsDto,
            comment, saveAsRecipe, recipeName) = command;
        
        var steps = (await _convertService.ConvertAsync(stepsDto, ct)).ToList();
        Recipe? recipe = null;
        if (saveAsRecipe)
        {
            if (recipeName is null)
            {
                throw new InvalidRecipeNameException(recipeName);
            }
            
            recipe = new Recipe(RecipeId.Create(), recipeName);
            foreach (var step in steps)
            {
                // assigns new id
                recipe.Steps.Add(AsRecipeStep(step, recipe));
            }
            await _recipeRepository.AddAsync(recipe, ct);
        }
        else if (recipeId is not null)
        {
            recipe = await _recipeRepository.GetAsync(recipeId, ct);
        }

        var tags = await _convertService.ConvertAsync(tagsDto.Select(t => new TagId(t)), ct);
        var project = await _projectRepository.GetAsync(projectId, ct) ?? throw new ProjectNotFoundException(projectId);
        var sample = new Core.Entities.Sample(sampleId,
            project,
            recipe,
            new Comment(comment),
            steps.ToList(),
            tags.ToList());
        project.Samples.Add(sample);
        await _sampleRepository.AddAsync(sample, ct);
    }
    
    private static RecipeStep AsRecipeStep(Step step, Recipe recipe)
    {
        var recipeStep = new RecipeStep(StepId.Create(), step.Comment, step.Parameters, recipe);
        recipe.AddStep(recipeStep);
        return recipeStep;
    }
}
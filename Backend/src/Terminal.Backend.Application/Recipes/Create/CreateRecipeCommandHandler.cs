using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Create;

internal sealed class CreateRecipeCommandHandler(
    IConvertDtoService convertDtoService,
    IRecipeRepository recipeRepository)
    : IRequestHandler<CreateRecipeCommand>
{
    public async Task Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var (id, name, steps) = request;

        var recipe = new Core.Entities.Recipe(id, name);
        foreach (var step in await convertDtoService.ConvertAsync(steps, cancellationToken))
        {
            recipe.Steps.Add(new RecipeStep(StepId.Create(), step.Comment, step.Parameters, recipe));
        }

        await recipeRepository.AddAsync(recipe, cancellationToken);
    }
}
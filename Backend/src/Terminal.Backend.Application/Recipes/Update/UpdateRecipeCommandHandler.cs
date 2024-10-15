using Terminal.Backend.Application.Common.Services;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Recipes.Update;

internal sealed class UpdateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IConvertDtoService convertDtoService)
    : IRequestHandler<UpdateRecipeCommand>
{
    public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var (id, name, stepsDto) = request;
        var recipe = await recipeRepository.GetAsync(id, cancellationToken);
        if (recipe is null)
        {
            throw new RecipeNotFoundException();
        }

        if (recipe.Name != name && !await recipeRepository.IsNameUniqueAsync(name, cancellationToken))
        {
            throw new InvalidRecipeNameException(name);
        }

        var steps = (await convertDtoService.ConvertAsync(stepsDto, cancellationToken))
            .Select(s => new RecipeStep(s.Id, s.Comment, s.Values, recipe));
        recipe.Update(name, steps);

        await recipeRepository.UpdateAsync(recipe, cancellationToken);
    }
}

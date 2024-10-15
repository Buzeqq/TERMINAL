using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Recipes.Delete;

internal sealed class DeleteRecipeCommandHandler(IRecipeRepository recipeRepository)
    : IRequestHandler<DeleteRecipeCommand>
{
    public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var recipe = await recipeRepository.GetAsync(id, cancellationToken);
        if (recipe is null)
        {
            throw new RecipeNotFoundException();
        }

        await recipeRepository.DeleteAsync(recipe, cancellationToken);
    }
}

using MediatR;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Recipe.Delete;

internal sealed class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
{
    private readonly IRecipeRepository _recipeRepository;

    public DeleteRecipeCommandHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var recipe = await _recipeRepository.GetAsync(id, cancellationToken);
        if (recipe is null)
        {
            throw new RecipeNotFoundException();
        }

        await _recipeRepository.DeleteAsync(recipe, cancellationToken);
    }
}
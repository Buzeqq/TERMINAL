using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Commands.Recipe.Update;

internal sealed class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IConvertDtoService _convertDtoService;

    public UpdateRecipeCommandHandler(IRecipeRepository recipeRepository, IConvertDtoService convertDtoService)
    {
        _recipeRepository = recipeRepository;
        _convertDtoService = convertDtoService;
    }

    public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var (id, name, stepsDto) = request;
        var recipe = await _recipeRepository.GetAsync(id, cancellationToken);
        if (recipe is null)
        {
            throw new RecipeNotFoundException();
        }

        if (recipe.RecipeName != name && !await _recipeRepository.IsNameUniqueAsync(name, cancellationToken))
        {
            throw new InvalidRecipeNameException(name);
        }

        var steps = (await _convertDtoService.ConvertAsync(stepsDto, cancellationToken))
            .Select(s => new RecipeStep(s.Id, s.Comment, s.Parameters, recipe));
        recipe.Update(name, steps);

        await _recipeRepository.UpdateAsync(recipe, cancellationToken);
    }
}
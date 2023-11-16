using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IRecipeRepository
{
    Task<Recipe?> GetAsync(RecipeId recipeId, CancellationToken ct);
}
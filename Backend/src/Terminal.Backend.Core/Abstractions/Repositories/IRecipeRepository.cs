using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IRecipeRepository
{
    Task<Recipe?> GetAsync(RecipeId recipeId, CancellationToken ct);
    Task AddAsync(Recipe recipe, CancellationToken ct);
    Task DeleteAsync(Recipe recipe, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(RecipeName name, CancellationToken cancellationToken);
    Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken);
}
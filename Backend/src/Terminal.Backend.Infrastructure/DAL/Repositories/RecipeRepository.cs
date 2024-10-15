using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class RecipeRepository(TerminalDbContext dbContext) : IRecipeRepository
{
    private readonly DbSet<Recipe> _recipes = dbContext.Recipes;

    public Task<Recipe?> GetAsync(RecipeId recipeId, CancellationToken cancellationToken)
        =>
            _recipes
            .Include(r => r.Steps)
            .ThenInclude(s => s.Values)
            .ThenInclude(p => p.Parameter)
            .SingleOrDefaultAsync(r => r.Id == recipeId, cancellationToken);

    public async Task AddAsync(Recipe recipe, CancellationToken cancellationToken) => await _recipes.AddAsync(recipe, cancellationToken);

    public Task DeleteAsync(Recipe recipe, CancellationToken cancellationToken)
    {
        _recipes.Remove(recipe);
        return Task.CompletedTask;
    }

    public Task<bool> IsNameUniqueAsync(RecipeName name, CancellationToken cancellationToken)
        =>
            _recipes.AllAsync(r => r.Name != name, cancellationToken);

    public Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken)
    {
        _recipes.Update(recipe);
        return Task.CompletedTask;
    }
}

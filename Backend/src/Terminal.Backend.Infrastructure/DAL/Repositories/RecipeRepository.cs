using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class RecipeRepository(TerminalDbContext dbContext) : IRecipeRepository
{
    private readonly DbSet<Recipe> _recipes = dbContext.Recipes;

    public Task<Recipe?> GetAsync(RecipeId recipeId, CancellationToken ct)
        =>
            this._recipes
            .Include(r => r.Steps)
            .ThenInclude(s => s.Parameters)
            .ThenInclude(p => p.Parameter)
            .SingleOrDefaultAsync(r => r.Id == recipeId, ct);

    public async Task AddAsync(Recipe recipe, CancellationToken ct) => await this._recipes.AddAsync(recipe, ct);

    public Task DeleteAsync(Recipe recipe, CancellationToken cancellationToken)
    {
        this._recipes.Remove(recipe);
        return Task.CompletedTask;
    }

    public Task<bool> IsNameUniqueAsync(RecipeName name, CancellationToken cancellationToken) 
        =>
            this._recipes.AllAsync(r => r.RecipeName != name, cancellationToken);

    public Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken)
    {
        this._recipes.Update(recipe);
        return Task.CompletedTask;
    }
}
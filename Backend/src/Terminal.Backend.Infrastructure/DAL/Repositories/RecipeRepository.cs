using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class RecipeRepository : IRecipeRepository
{
    private readonly DbSet<Recipe> _recipes;

    public RecipeRepository(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public Task<Recipe?> GetAsync(RecipeId recipeId, CancellationToken ct)
        => _recipes.SingleOrDefaultAsync(r => r.Id == recipeId, ct);
}
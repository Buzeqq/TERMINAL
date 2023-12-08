using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class StepsRepository : IStepsRepository
{
    private readonly DbSet<Recipe> _recipes;

    public StepsRepository(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public async Task<IEnumerable<RecipeStep>> GetFromRecipeAsync(RecipeId id, CancellationToken ct)
        => (await _recipes
            .Include(r => r.Steps)
            .SingleOrDefaultAsync(r => r.Id == id, ct))?.Steps ?? new List<RecipeStep>();
}
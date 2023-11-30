using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IStepsRepository
{
    Task<IEnumerable<RecipeStep>> GetFromRecipeAsync(RecipeId id, CancellationToken ct);
}
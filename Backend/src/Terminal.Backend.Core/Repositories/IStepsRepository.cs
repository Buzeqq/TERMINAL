using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Repositories;

public interface IStepsRepository
{
    Task<IEnumerable<Step>> GetFromRecipeAsync(RecipeId id, CancellationToken ct);
}
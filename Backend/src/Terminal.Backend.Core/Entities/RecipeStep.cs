using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class RecipeStep : Step
{
    public Recipe Recipe { get; set; } = null!;

    public RecipeStep(StepId id, Comment comment, Recipe recipe) : base(id, comment) => Recipe = recipe;

    public RecipeStep(StepId id, Comment comment, ICollection<ParameterValue> parameters, Recipe recipe) : base(id,
        comment, parameters) =>
        Recipe = recipe;


    // Entity Framework constructor
    // ReSharper disable once UnusedMember.Local
    private RecipeStep(StepId id, Comment comment) : base(id, comment) { }
}

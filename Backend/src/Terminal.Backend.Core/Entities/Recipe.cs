using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Recipe(RecipeId id, RecipeName name)
{
    public RecipeId Id { get; private set; } = id;
    public RecipeName Name { get; private set; } = name;

    public ICollection<RecipeStep> Steps { get; } = [];

    public void AddStep(RecipeStep step)
    {
        Steps.Add(step);
        step.Recipe = this;
    }

    public void Update(RecipeName name, IEnumerable<RecipeStep> steps)
    {
        Name = name;

        var mergedSteps = Steps.Join(steps, s => s.Id, s => s.Id,
            (s1, s2) => new Tuple<RecipeStep, RecipeStep>(s1, s2));
        foreach (var (oldStep, newStep) in mergedSteps)
        {
            oldStep.Update(newStep.Values, newStep.Comment);
        }
    }
}

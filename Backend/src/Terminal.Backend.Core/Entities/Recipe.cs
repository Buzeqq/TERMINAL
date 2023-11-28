using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Recipe
{
    public RecipeId Id { get; private set; }
    public RecipeName RecipeName { get; private set; }
    
    public ICollection<RecipeStep> Steps { get; private set; } = new List<RecipeStep>();

    public Recipe(RecipeId id, RecipeName recipeName)
    {
        Id = id;
        RecipeName = recipeName;
    }

    public void AddStep(RecipeStep step)
    {
        Steps.Add(step);
        step.Recipe = this;
    }
}
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Recipe
{
    public RecipeId Id { get; private set; }
    public RecipeName RecipeName { get; private set; }
    
    public ICollection<Step> Steps { get; private set; } = new List<Step>();

    public Recipe(RecipeId id, RecipeName recipeName)
    {
        Id = id;
        RecipeName = recipeName;
    }

    public void AddStep(Step step)
    {
        Steps.Add(step);
    }
}
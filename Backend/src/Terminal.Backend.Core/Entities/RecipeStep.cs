using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public class RecipeStep : Step
{
    public Recipe? Recipe { get; private set; }
    
    public RecipeStep(StepId id, Comment comment, ICollection<ParameterValue> parameters) : base(id, comment, parameters)
    {
    }
}
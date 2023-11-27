using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public class Step
{
    public StepId Id { get; private set; }
    public Comment Comment { get; private set; }
    public ICollection<ParameterValue> Parameters { get; private set; } = new List<ParameterValue>();

    public Step(StepId id, Comment comment, ICollection<ParameterValue> parameters)
    {
        Id = id;
        Comment = comment;
        Parameters = parameters;
    }

    private Step(StepId id, Comment comment)
    {
        Id = id;
        Comment = comment;
    }
}
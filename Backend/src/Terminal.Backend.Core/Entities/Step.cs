using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public abstract class Step
{
    public StepId Id { get; private set; }
    public Comment Comment { get; private set; }
    public ICollection<ParameterValue> Parameters { get; private set; } = new List<ParameterValue>();

    protected Step(StepId id, Comment comment, ICollection<ParameterValue> parameters)
    {
        Id = id;
        Comment = comment;
        Parameters = parameters;
    }

    protected Step(StepId id, Comment comment)
    {
        Id = id;
        Comment = comment;
    }

    public void Update(IEnumerable<ParameterValue> parameters, Comment comment)
    {
        Comment = comment;
        var mergedParameters = Parameters.Join(parameters, p => p.Parameter.Id,
            p => p.Parameter.Id, (p1, p2) => new Tuple<ParameterValue, ParameterValue>(p1, p2));
        foreach (var (oldParameterValue, newParameterValue) in mergedParameters)
        {
            oldParameterValue.Update(newParameterValue);
        }
    }
}

public sealed class SampleStep : Step
{
    public SampleStep(StepId id, Comment comment) : base(id, comment)
    {
    }

    public SampleStep(StepId id, Comment comment, ICollection<ParameterValue> parameters) : base(id, comment,
        parameters)
    {
    }
}
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
        this.Id = id;
        this.Comment = comment;
        this.Parameters = parameters;
    }

    protected Step(StepId id, Comment comment)
    {
        this.Id = id;
        this.Comment = comment;
    }

    public void Update(IEnumerable<ParameterValue> parameters, Comment comment)
    {
        this.Comment = comment;
        var mergedParameters = this.Parameters.Join(parameters, p => p.Parameter.Id,
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
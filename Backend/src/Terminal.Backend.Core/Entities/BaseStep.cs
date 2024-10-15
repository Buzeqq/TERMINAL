using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public abstract class BaseStep
{
    public StepId Id { get; private set; }
    public Comment Comment { get; private set; }
    public ICollection<ParameterValue> Values { get; private set; } = new List<ParameterValue>();

    protected BaseStep(StepId id, Comment comment, ICollection<ParameterValue> values)
    {
        Id = id;
        Comment = comment;
        Values = values;
    }

    protected BaseStep(StepId id, Comment comment)
    {
        Id = id;
        Comment = comment;
    }

    public void Update(IEnumerable<ParameterValue> parameters, Comment comment)
    {
        Comment = comment;
        var mergedParameters = Values.Join(parameters, p => p.Parameter.Id,
            p => p.Parameter.Id, (p1, p2) => new Tuple<ParameterValue, ParameterValue>(p1, p2));
        foreach (var (oldParameterValue, newParameterValue) in mergedParameters)
        {
            oldParameterValue.Update(newParameterValue);
        }
    }
}

public sealed class SampleStep : BaseStep
{
    public SampleStep(StepId id, Comment comment) : base(id, comment)
    {
    }

    public SampleStep(StepId id, Comment comment, ICollection<ParameterValue> values) : base(id, comment,
        values)
    {
    }
}

using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Step
{
    public Guid Id { get; private set; }
    public ICollection<ParameterValue> Parameters { get; private set; } = new List<ParameterValue>();
    public Comment Comment { get; private set; }
}
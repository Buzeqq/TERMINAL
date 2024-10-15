using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public abstract class ParameterValue(ParameterValueId id)
{
    public ParameterValueId Id { get; private set; } = id;

    public abstract Parameter Parameter { get; }

    public abstract T Accept<T>(IParameterValueVisitor<T> visitor);

    public abstract void Update(ParameterValue newParameterValue);

    protected ParameterValue(ParameterValue parameterValue) : this(ParameterValueId.Create()) { }
}

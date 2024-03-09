using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public abstract class ParameterValue(ParameterValueId id, Parameter parameter)
{
    public ParameterValueId Id { get; private set; } = id;

    public Parameter Parameter { get; private set; } = parameter;

    protected ParameterValue(ParameterValueId id) : this(id, null)
    {
    }

    public abstract ParameterValue DeepCopy(ParameterValueId id);

    public abstract void Update(ParameterValue newParameterValue);
}
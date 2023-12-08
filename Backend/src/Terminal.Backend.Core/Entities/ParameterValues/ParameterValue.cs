using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public abstract class ParameterValue
{
    public ParameterValueId Id { get; private set; }

    public Parameter Parameter { get; private set; }

    protected ParameterValue(ParameterValueId id, Parameter parameter)
    {
        Id = id;
        Parameter = parameter;
    }

    protected ParameterValue(ParameterValueId id)
    {
        Id = id;
    }

    public abstract ParameterValue DeepCopy(ParameterValueId id);

    public abstract void Update(ParameterValue newParameterValue);
}
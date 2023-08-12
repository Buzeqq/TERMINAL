using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class IntegerParameterValue : ParameterValue
{
    public int Value { get; private set; }

    public IntegerParameterValue(ParameterValueId id, Parameter parameter, int value) : base(id, parameter)
    {
        Value = value;
    }

    private IntegerParameterValue(ParameterValueId id, int value) : base(id)
    {
        Value = value;
    }
}
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class DecimalParameterValue : ParameterValue
{
    public decimal Value { get; private set; }

    public DecimalParameterValue(ParameterValueId id, Parameter parameter, decimal value) : base(id, parameter)
    {
        Value = value;
    }

    private DecimalParameterValue(ParameterValueId id, decimal value) : base(id)
    {
        Value = value;
    }
}
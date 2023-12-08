using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class DecimalParameterValue : ParameterValue
{
    public decimal Value { get; private set; }

    public DecimalParameterValue(ParameterValueId id, DecimalParameter parameter, decimal value) : base(id, parameter)
    {
        if (value % parameter.Step != 0)
        {
            throw new DecimalParameterValueNotValidException(parameter, value);
        }

        Value = value;
    }

    private DecimalParameterValue(ParameterValueId id, decimal value) : base(id)
    {
        Value = value;
    }

    public override ParameterValue DeepCopy(ParameterValueId id)
    {
        return new DecimalParameterValue(id,
            Parameter as DecimalParameter
            ?? throw new ParameterValueCopyException(typeof(DecimalParameter), Parameter.GetType()),
            Value);
    }

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not DecimalParameterValue newDecimalParameterValue) return;

        var decimalParameter = (DecimalParameter)Parameter;
        var value = newDecimalParameterValue.Value;
        if (value % decimalParameter.Step != 0)
        {
            throw new DecimalParameterValueNotValidException(decimalParameter, value);
        }

        Value = value;
    }
}
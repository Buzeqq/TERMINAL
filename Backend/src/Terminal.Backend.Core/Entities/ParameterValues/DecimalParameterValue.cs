using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class DecimalParameterValue : ParameterValue
{
    public DecimalParameter DecimalParameter { get; private set; }
    public decimal Value { get; private set; }

    public DecimalParameterValue(ParameterValueId id, DecimalParameter parameter, decimal value) : base(id)
    {
        if (value % parameter.Step != 0)
        {
            throw new DecimalParameterValueNotValidException(parameter, value);
        }

        Value = value;
        DecimalParameter = parameter;
    }

    public DecimalParameterValue(DecimalParameterValue parameterValue) : base(parameterValue)
    {
        Value = parameterValue.Value;
        DecimalParameter = parameterValue.DecimalParameter;
    }

    public override Parameter Parameter => DecimalParameter;

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not DecimalParameterValue newDecimalParameterValue)
        {
            return;
        }

        var decimalParameter = DecimalParameter;
        var value = newDecimalParameterValue.Value;
        if (value % decimalParameter.Step != 0)
        {
            throw new DecimalParameterValueNotValidException(decimalParameter, value);
        }

        Value = value;
    }

    public override T Accept<T>(IParameterValueVisitor<T> visitor) => visitor.Visit(this);

    private DecimalParameterValue(ParameterValueId id, decimal value) : base(id) => Value = value;
}

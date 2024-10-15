using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class IntegerParameterValue : ParameterValue
{
    public IntegerParameter IntegerParameter { get; private set; }
    public int Value { get; private set; }

    public IntegerParameterValue(ParameterValueId id, IntegerParameter parameter, int value) : base(id)
    {
        if (value % parameter.Step != 0)
        {
            throw new IntegerParameterValueNotValidException(parameter, value);
        }

        Value = value;
        IntegerParameter = parameter;
    }

    public IntegerParameterValue(IntegerParameterValue parameterValue) : base(parameterValue)
    {
        Value = parameterValue.Value;
        IntegerParameter = parameterValue.IntegerParameter;
    }

    public override Parameter Parameter => IntegerParameter;

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not IntegerParameterValue newIntegerParameterValue)
        {
            return;
        }

        var integerParameter = IntegerParameter;
        var value = newIntegerParameterValue.Value;
        if (value % integerParameter.Step != 0)
        {
            throw new IntegerParameterValueNotValidException(integerParameter, value);
        }

        Value = value;
    }

    public override T Accept<T>(IParameterValueVisitor<T> visitor) => visitor.Visit(this);

    private IntegerParameterValue(ParameterValueId id, int value) : base(id) => Value = value;
}

using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class IntegerParameterValue : ParameterValue
{
    public int Value { get; private set; }

    public IntegerParameterValue(ParameterValueId id, IntegerParameter parameter, int value) : base(id, parameter)
    {
        if (value % parameter.Step != 0)
        {
            throw new IntegerParameterValueNotValidException(parameter, value);
        }

        this.Value = value;
    }

    private IntegerParameterValue(ParameterValueId id, int value) : base(id) => this.Value = value;

    public override ParameterValue DeepCopy(ParameterValueId id) =>
        new IntegerParameterValue(id, this.Parameter as IntegerParameter
            ?? throw new ParameterValueCopyException(typeof(IntegerParameter), this.Parameter.GetType()), this.Value);

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not IntegerParameterValue newIntegerParameterValue)
        {
            return;
        }

        var integerParameter = (IntegerParameter)this.Parameter;
        var value = newIntegerParameterValue.Value;
        if (value % integerParameter.Step != 0)
        {
            throw new IntegerParameterValueNotValidException(integerParameter, value);
        }

        this.Value = value;
    }
}
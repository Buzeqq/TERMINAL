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

        this.Value = value;
    }

    private DecimalParameterValue(ParameterValueId id, decimal value) : base(id) => this.Value = value;

    public override ParameterValue DeepCopy(ParameterValueId id) =>
        new DecimalParameterValue(id, this.Parameter as DecimalParameter
            ?? throw new ParameterValueCopyException(typeof(DecimalParameter), this.Parameter.GetType()), this.Value);

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not DecimalParameterValue newDecimalParameterValue)
        {
            return;
        }

        var decimalParameter = (DecimalParameter)this.Parameter;
        var value = newDecimalParameterValue.Value;
        if (value % decimalParameter.Step != 0)
        {
            throw new DecimalParameterValueNotValidException(decimalParameter, value);
        }

        this.Value = value;
    }
}
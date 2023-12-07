using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class TextParameterValue : ParameterValue
{
    public string Value { get; private set; }

    public TextParameterValue(ParameterValueId id, TextParameter parameter, string value) : base(id, parameter)
    {
        if (!parameter.AllowedValues.Contains(value))
        {
            throw new TextParameterValueNotValidException(parameter, value);
        }

        Value = value;
    }

    private TextParameterValue(ParameterValueId id, string value) : base(id)
    {
        Value = value;
    }

    public override ParameterValue DeepCopy(ParameterValueId id)
    {
        return new TextParameterValue(id,
            Parameter as TextParameter
            ?? throw new ParameterValueCopyException(typeof(TextParameterValue), Parameter.GetType()),
            Value);
    }

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not TextParameterValue newTextParameterValue) return;
        var textParameter = (TextParameter)Parameter;
        var value = newTextParameterValue.Value;
        if (!textParameter.AllowedValues.Contains(value))
        {
            throw new TextParameterValueNotValidException(textParameter, value);
        }

        Value = value;
    }
}
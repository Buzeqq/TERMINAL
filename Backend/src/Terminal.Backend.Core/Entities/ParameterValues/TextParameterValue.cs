using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class TextParameterValue : ParameterValue
{
    public TextParameter TextParameter { get; private set; }
    public string Value { get; private set; }

    public TextParameterValue(ParameterValueId id, TextParameter parameter, string value) : base(id)
    {
        if (!parameter.AllowedValues.Contains(value))
        {
            throw new TextParameterValueNotValidException(parameter, value);
        }

        Value = value;
        TextParameter = parameter;
    }

    public TextParameterValue(TextParameterValue parameterValue) : base(parameterValue)
    {
        TextParameter = parameterValue.TextParameter;
        Value = parameterValue.Value;
    }

    public override Parameter Parameter => TextParameter;

    public override void Update(ParameterValue newParameterValue)
    {
        if (newParameterValue is not TextParameterValue newTextParameterValue)
        {
            return;
        }

        var textParameter = TextParameter;
        var value = newTextParameterValue.Value;
        if (!textParameter.AllowedValues.Contains(value))
        {
            throw new TextParameterValueNotValidException(textParameter, value);
        }

        Value = value;
    }

    public override T Accept<T>(IParameterValueVisitor<T> visitor) => visitor.Visit(this);

    private TextParameterValue(ParameterValueId id, string value) : base(id) => Value = value;
}

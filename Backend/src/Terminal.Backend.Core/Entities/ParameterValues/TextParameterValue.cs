using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class TextParameterValue : ParameterValue
{
    public string Value { get; private set; }

    public TextParameterValue(TextParameter parameter, string value) : base(ParameterValueId.Create(), parameter)
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
}
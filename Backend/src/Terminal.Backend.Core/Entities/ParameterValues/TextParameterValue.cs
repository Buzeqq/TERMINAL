using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class TextParameterValue : ParameterValue
{
    public string Value { get; private set; }

    public TextParameterValue(ParameterValueId id, Parameter parameter, string value) : base(id, parameter)
    {
        Value = value;
    }

    private TextParameterValue(ParameterValueId id, string value) : base(id)
    {
        Value = value;
    }
}
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class TextParameter : Parameter
{
    public List<string> AllowedValues { get; private set; }

    public TextParameter() { }

    public TextParameter(ParameterName id, List<string> allowedValues, bool isActive = true) : base(id, isActive)
    {
        AllowedValues = allowedValues;
    }
}
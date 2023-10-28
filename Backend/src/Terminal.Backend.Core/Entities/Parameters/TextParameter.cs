using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class TextParameter : Parameter
{
    public List<string> AllowedValues { get; private set; }

    public TextParameter(ParameterName name, List<string> allowedValues, bool isActive = true) : base(name, isActive)
    {
        AllowedValues = allowedValues;
    }
}
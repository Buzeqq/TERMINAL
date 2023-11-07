using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class TextParameter : Parameter
{
    public List<string> AllowedValues { get; private set; }

    public TextParameter(ParameterId id, ParameterName name, List<string> allowedValues, bool isActive = true) : base(id, name, isActive)
    {
        AllowedValues = allowedValues;
    }
}
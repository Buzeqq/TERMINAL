using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class TextParameter : Parameter
{
    public IEnumerable<string> AllowedValues { get; private set; }

    public TextParameter(ParameterName id, IEnumerable<string> allowedValues, bool isActive = true) : base(id, isActive)
    {
        AllowedValues = allowedValues;
    }

    private TextParameter(List<string> allowedValues, bool isActive = true) : base(isActive)
    {
        AllowedValues = allowedValues ?? throw new ArgumentNullException(nameof(allowedValues));
    }
}
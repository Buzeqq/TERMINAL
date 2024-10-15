using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class TextParameter(
    ParameterId id,
    ParameterName name,
    ParameterId? parentId,
    List<string> allowedValues,
    uint defaultValue = 0,
    uint order = 0,
    bool isActive = true)
    : Parameter(id, name, parentId, order, isActive)
{
    public List<string> AllowedValues { get; private set; } = allowedValues;
    public uint DefaultValue { get; private set; } = defaultValue;
}

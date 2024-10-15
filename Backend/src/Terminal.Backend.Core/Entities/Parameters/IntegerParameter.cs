using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class IntegerParameter(
    ParameterId id,
    ParameterName name,
    ParameterId? parentId,
    string unit,
    int step,
    int defaultValue = 0,
    uint order = 0,
    bool isActive = true)
    : NumericParameter(id, name, parentId, unit, order, isActive)
{
    public int Step { get; private set; } = step;
    public int DefaultValue { get; private set; } = defaultValue;
}

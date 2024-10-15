using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class DecimalParameter(
    ParameterId id,
    ParameterName name,
    ParameterId? parentId,
    string unit,
    decimal step,
    decimal defaultValue = 0,
    uint order = 0,
    bool isActive = true)
    : NumericParameter(id, name, parentId, unit, order, isActive)
{
    public decimal Step { get; private set; } = step;
    public decimal DefaultValue { get; private set; } = defaultValue;
}

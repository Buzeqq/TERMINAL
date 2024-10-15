using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class NumericParameter(
    ParameterId id,
    ParameterName name,
    ParameterId? parentId,
    string unit,
    uint order = 0,
    bool isActive = true)
    : Parameter(id, name, parentId, order, isActive)
{
    public string Unit { get; private set; } = unit;
}

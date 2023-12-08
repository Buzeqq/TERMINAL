using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class NumericParameter : Parameter
{
    public string Unit { get; private set; }

    protected NumericParameter(ParameterId id, ParameterName name, string unit, uint order = 0, bool isActive = true)
        : base(id, name, order, isActive)
    {
        Unit = unit;
    }
}
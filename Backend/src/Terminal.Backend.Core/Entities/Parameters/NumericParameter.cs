using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class NumericParameter : Parameter
{
    public string Unit { get; private set; }

    protected NumericParameter(ParameterName name, string unit, bool isActive) : base(name, isActive)
    {
        Unit = unit;
    }
}
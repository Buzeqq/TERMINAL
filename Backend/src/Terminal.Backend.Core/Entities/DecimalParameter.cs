using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class DecimalParameter : Parameter
{
    public string Unit { get; private set; }
    public decimal Step { get; private set; }
    
    public DecimalParameter() {}

    public DecimalParameter(ParameterName name, string unit, decimal step, bool isActive = true) : base(name, isActive)
    {
        Unit = unit;
        Step = step;
    }
}
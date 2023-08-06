using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class IntegerParameter : Parameter
{
    public string Unit { get; private set; }
    public int Step { get; private set; }
    
    public IntegerParameter() {}

    public IntegerParameter(ParameterName name, string unit, int step, bool isActive = true) : base(name, isActive)
    {
        Unit = unit;
        Step = step;
    }
}
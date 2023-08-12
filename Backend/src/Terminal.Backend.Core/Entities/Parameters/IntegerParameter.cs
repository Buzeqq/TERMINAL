using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class IntegerParameter : NumericParameter
{
    public int Step { get; private set; }

    public IntegerParameter(ParameterName name, string unit, int step, bool isActive = true) : 
        base(name, unit, isActive)
    {
        Step = step;
    }
}
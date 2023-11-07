using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public sealed class DecimalParameter : NumericParameter
{
    public decimal Step { get; private set; }

    public DecimalParameter(ParameterId id, ParameterName name, string unit, decimal step, bool isActive = true) : 
        base(id, name, unit, isActive)
    {
        Step = step;
    }
}
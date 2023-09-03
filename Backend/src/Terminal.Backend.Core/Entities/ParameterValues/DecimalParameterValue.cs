using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class DecimalParameterValue : ParameterValue
{
    public decimal Value { get; private set; }

    public DecimalParameterValue(DecimalParameter parameter, decimal value) : base(ParameterValueId.Create(), parameter)
    {
        if (value % parameter.Step != 0)
        {
            throw new DecimalParameterValueNotValidException(parameter, value);
        }
        
        Value = value;
    }

    private DecimalParameterValue(ParameterValueId id, decimal value) : base(id)
    {
        Value = value;
    }
}
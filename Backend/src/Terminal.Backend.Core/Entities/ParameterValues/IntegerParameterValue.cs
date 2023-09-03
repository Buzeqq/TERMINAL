using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.ParameterValues;

public sealed class IntegerParameterValue : ParameterValue
{
    public int Value { get; private set; }

    public IntegerParameterValue(IntegerParameter parameter, int value) : base(ParameterValueId.Create(), parameter)
    {
        if (value % parameter.Step != 0)
        {
            throw new IntegerParameterValueNotValidException(parameter, value);
        }
        
        Value = value;
    }

    private IntegerParameterValue(ParameterValueId id, int value) : base(id)
    {
        Value = value;
    }
}
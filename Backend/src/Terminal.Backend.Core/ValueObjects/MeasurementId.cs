using System.Numerics;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record MeasurementId
{
    private BigInteger _value;
    private const string _prefix = "AX";

    public string Value => $"{_prefix}{_value}";

    public MeasurementId(BigInteger value)
    {
        _value = value;
    }
    
}
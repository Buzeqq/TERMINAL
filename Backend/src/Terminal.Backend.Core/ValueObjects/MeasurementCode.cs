using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record MeasurementCode
{
    private const string Prefix = "AX"; // TODO: move to configuration file
    public string Value => $"{Prefix}{Number}";
    public ulong Number { get; }

    public MeasurementCode(ulong number)
    {
        Number = number;
    }

    public MeasurementCode(string code)
    {
        var isParsable = ulong.TryParse(code.AsSpan(Prefix.Length), out var number);
        var isValid = !string.IsNullOrWhiteSpace(code) &&
                      code.StartsWith(Prefix) &&
                      isParsable;
        
        if (!isValid)
        {
            throw new InvalidMeasurementCodeException(code);
        }

        Number = number;
    }
    
    public static implicit operator string(MeasurementCode code) => code.Value;
    public static implicit operator MeasurementCode(string code) => new(code);
    
}

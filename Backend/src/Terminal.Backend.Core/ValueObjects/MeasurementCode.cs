using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record MeasurementCode
{
    private static readonly string Prefix = "AX"; // TODO: move to configuration file
    public string Value => $"{Prefix}{Number}";
    public ulong Number { get; }

    public MeasurementCode() { }

    public MeasurementCode(ulong number)
    {
        Number = number;
    }

    public MeasurementCode(string code)
    {
        ulong number = 0; // should never create code with 0 number, it's here only so compiler stops complaining
        var isValid = !string.IsNullOrWhiteSpace(code) &&
                      code.StartsWith(Prefix) &&
                      ulong.TryParse(code.AsSpan(Prefix.Length), out number) &&
                      number != 0;
        
        if (!isValid)
        {
            throw new InvalidMeasurementCodeException(code);
        }

        Number = number;
    }
}

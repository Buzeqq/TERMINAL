using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record SampleCode
{
    private const string Prefix = "AX"; // TODO: move to configuration file
    public string Value => $"{Prefix}{Number}";
    public ulong Number { get; }

    public SampleCode(ulong number) => Number = number;

    public SampleCode(string code)
    {
        var isParsable = ulong.TryParse(code.AsSpan(Prefix.Length), out var number);
        var isValid = !string.IsNullOrWhiteSpace(code) &&
                      code.StartsWith(Prefix, StringComparison.Ordinal) &&
                      isParsable;

        if (!isValid)
        {
            throw new InvalidSampleCodeException(code);
        }

        Number = number;
    }

    public static implicit operator string(SampleCode code) => code.Value;
    public static implicit operator SampleCode(string code) => new(code);
}

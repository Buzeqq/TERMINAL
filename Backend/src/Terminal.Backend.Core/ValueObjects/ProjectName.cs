using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record ProjectName
{
    public string Value { get; }

    public ProjectName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidProjectNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(ProjectName name) => name.Value;
    public static implicit operator ProjectName(string value) => new(value);
}

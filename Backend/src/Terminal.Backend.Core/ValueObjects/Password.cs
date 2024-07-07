using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPasswordException();
        }

        Value = value;
    }

    public static implicit operator string(Password password) => password.Value;
    public static implicit operator Password(string value) => new(value);
}

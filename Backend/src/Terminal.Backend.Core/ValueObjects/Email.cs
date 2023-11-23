using System.ComponentModel.DataAnnotations;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!new EmailAddressAttribute().IsValid(value))
        {
            throw new InvalidEmailException(value);
        }

        Value = value;
    }

    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string value) => new(value);
}
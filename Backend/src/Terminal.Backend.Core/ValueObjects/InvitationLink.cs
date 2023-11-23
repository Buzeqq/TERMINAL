namespace Terminal.Backend.Core.ValueObjects;

public sealed record InvitationLink(string Value)
{
    public static implicit operator string(InvitationLink email) => email.Value;
    public static implicit operator InvitationLink(string value) => new(value);
}
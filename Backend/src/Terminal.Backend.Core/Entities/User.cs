using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
}
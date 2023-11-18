using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public bool Activated { get; private set; }
    
    public User(UserId id, Email email, Password password, bool activated)
    {
        Id = id;
        Email = email;
        Password = password;
        Activated = activated;
    }

    public void SetRole(Role role)
    {
        Role = role;
        role.Users.Add(this);
    }
}
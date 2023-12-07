using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public bool Activated { get; private set; }

    private User(UserId id, Email email, Password password, bool activated)
    {
        Id = id;
        Email = email;
        Password = password;
        Activated = activated;
    }

    public static User CreateInactiveUser(UserId id, Email email) =>
        new(id, email, new Password("not-active-user"), false);

    public static User CreateActiveUser(UserId id, Email email, Password password)
        => new(id, email, password, true);

    public void SetRole(Role role)
    {
        Role = role;
        role.Users.Add(this);
    }

    public void UpdatePassword(Password password)
    {
        Password = password;
    }

    public void Activate()
    {
        Activated = true;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }
}
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Invitation
{
    public InvitationId Id { get; private set; }
    public InvitationLink Link { get; private set; }
    public DateTime ExpiresIn { get; private set; }
    public User User { get; private set; }

    private Invitation(InvitationId id, InvitationLink link, DateTime expiresIn)
    {
        Id = id;
        Link = link;
        ExpiresIn = expiresIn;
    }

    public static Invitation CreateFor(User user, InvitationId id, InvitationLink link, DateTime expiresIn)
    {
        var invitation = new Invitation(id, link, expiresIn)
        {
            User = user
        };
        return invitation;
    }
}
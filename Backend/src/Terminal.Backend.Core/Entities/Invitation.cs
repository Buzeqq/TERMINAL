using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Invitation
{
    public InvitationId Id { get; private set; }
    public InvitationLink Link { get; private set; }
    public DateTime ExpiersIn { get; private set; }
    public User User { get; private set; }
}
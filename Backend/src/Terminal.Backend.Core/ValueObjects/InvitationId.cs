using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record InvitationId
{
    public Guid Value { get; }

    public InvitationId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static InvitationId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(InvitationId id) => id.Value;
    public static implicit operator InvitationId(Guid id) => new(id);
}
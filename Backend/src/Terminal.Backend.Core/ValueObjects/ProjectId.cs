using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record ProjectId
{
    public Guid Value { get; }

    public ProjectId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    private ProjectId()
    {
    }

    public static ProjectId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ProjectId id) => id.Value;
    public static implicit operator ProjectId(Guid id) => new(id);
}

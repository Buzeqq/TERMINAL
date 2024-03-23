namespace Terminal.Backend.Core.Entities;

using ValueObjects;

public sealed class Author(AuthorId id)
{
    public AuthorId Id { get; private set; } = id;
}

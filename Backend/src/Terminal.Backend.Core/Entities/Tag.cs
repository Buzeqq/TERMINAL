using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Tag
{
    public TagId Id { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    public Tag(TagId id, string name, bool isActive = true)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }

    public void ChangeStatus(bool isActive)
    {
        IsActive = isActive;
    }
}
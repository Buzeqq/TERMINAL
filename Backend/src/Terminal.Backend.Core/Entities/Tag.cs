using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Tag(TagId id, TagName name, bool isActive = true)
{
    public TagId Id { get; private set; } = id;
    public TagName Name { get; private set; } = name;
    public bool IsActive { get; private set; } = isActive;

    public void ChangeStatus(bool isActive) => IsActive = isActive;

    public void Update(string name) => Name = name;
}

using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Tag
{
    public TagName Name { get; private set; }
    public bool IsActive { get; private set; }

    public Tag(TagName name, bool isActive = true)
    {
        Name = name;
        IsActive = isActive;
    }

    public void ChangeStatus(bool status)
    {
        if (status == IsActive)
        {
            return;
        }

        IsActive = status;
    }
}
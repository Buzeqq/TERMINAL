using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Project(ProjectId id, string name, bool isActive = true)
{
    public ProjectId Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public bool IsActive { get; private set; } = isActive;

    public ICollection<Sample> Samples { get; } = [];

    public void ChangeStatus(bool isActive) => IsActive = isActive;

    public void Update(ProjectName name) => Name = name;
}

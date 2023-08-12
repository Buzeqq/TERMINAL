using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Project
{
    public ProjectId Id { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public ICollection<Measurement> Measurements { get; private set; } = new List<Measurement>();

    public Project(ProjectId id, string name, bool isActive = true)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }

    private Project(string name, bool isActive = true)
    {
        Name = name;
        IsActive = isActive;
    }
}
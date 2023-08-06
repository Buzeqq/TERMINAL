namespace Terminal.Backend.Core.Entities;

public sealed class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set;  }
    public bool IsActive { get; private set; }
    public ICollection<Measurement> Measurements { get; private set; } = new List<Measurement>();
}
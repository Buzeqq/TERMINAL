namespace Terminal.Backend.Core.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set;  }
    public bool IsActive { get; private set; }
}
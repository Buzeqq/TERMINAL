using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public abstract class Parameter
{
    public ParameterName Name { get; }
    public bool IsActive { get; private set; }
    public ICollection<ParameterValue> ParameterValues { get; private set; } = new List<ParameterValue>();
    
    protected Parameter() { }

    protected Parameter(ParameterName name, bool isActive = true)
    {
        Name = name;
        IsActive = true;
    }
}
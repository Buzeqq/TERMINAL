using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class Parameter
{
    public ParameterName Name { get; private set; }
    public bool IsActive { get; private set; }
    

    protected Parameter(ParameterName name, bool isActive = true)
    {
        Name = name;
        IsActive = isActive;
    }

    public void ChangeStatus(bool isActive)
    {
        IsActive = isActive;
    }
}
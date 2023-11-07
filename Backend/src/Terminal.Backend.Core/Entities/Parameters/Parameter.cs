using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class Parameter
{
    public ParameterId Id { get; private set; }
    public ParameterName Name { get; private set; }
    public bool IsActive { get; private set; }
    

    protected Parameter(ParameterId id, ParameterName name, bool isActive = true)
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

using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class Parameter
{
    public ParameterId Id { get; private set; }
    public ParameterName Name { get; private set; }
    public uint Order { get; private set; }
    public bool IsActive { get; private set; }

    public Parameter? Parent { get; private set; }

    protected Parameter(ParameterId id, ParameterName name, uint order = 0, bool isActive = true)
    {
        Id = id;
        Name = name;
        Order = order;
        IsActive = isActive;
    }

    public void ChangeStatus(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetParent(Parameter parent)
    {
        Parent = parent;
    }
}
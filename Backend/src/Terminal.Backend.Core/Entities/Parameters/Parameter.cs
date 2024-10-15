using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities.Parameters;

public abstract class Parameter(ParameterId id, ParameterName name, ParameterId? parentId, uint order = 0, bool isActive = true)
{
    public ParameterId Id { get; private set; } = id;
    public ParameterName Name { get; private set; } = name;
    public uint Order { get; private set; } = order;
    public bool IsActive { get; private set; } = isActive;

    public ParameterId? ParentId { get; private set; } = parentId;
    public Parameter? Parent { get; protected set; }

    public void ChangeStatus(bool isActive) => IsActive = isActive;

    public void SetParent(Parameter parent) => Parent = parent;
}

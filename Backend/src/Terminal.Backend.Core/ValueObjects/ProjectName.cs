using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record ProjectName
{
    public string Name { get; }

    public ProjectName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidProjectNameException(name);
        }

        Name = name;
    }

    public static implicit operator string(ProjectName name) => name.Name;
    public static implicit operator ProjectName(string value) => new(value);
}
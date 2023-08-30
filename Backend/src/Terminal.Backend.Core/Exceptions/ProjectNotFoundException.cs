using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class ProjectNotFoundException : TerminalException
{
    public ProjectNotFoundException(ProjectId id) : base($"Project with id: {id} not found")
    {
    }
}
namespace Terminal.Backend.Core.Exceptions;

public sealed class ProjectNotFoundException : TerminalException
{
    public ProjectNotFoundException() : base("Project not found!")
    {
    }
}
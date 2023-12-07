using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal class ProjectNotActiveException : TerminalException
{
    public ProjectNotActiveException(string projectName) : base($"Project {projectName} is not active!")
    {
    }
}
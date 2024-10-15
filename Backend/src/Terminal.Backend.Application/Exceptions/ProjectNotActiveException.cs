using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal class ProjectNotActiveException(string projectName)
    : TerminalException($"Project {projectName} is not active!");

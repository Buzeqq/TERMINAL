using System.Reflection;

namespace Terminal.Backend.Application;

/// <summary>
/// Class <c>AssemblyReference</c> is a helper class that shares reference to assembly.
/// </summary>
public static class AssemblyReference
{
    /// <value>
    /// Reference to assembly.
    /// </value>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

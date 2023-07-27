using System.Reflection;

namespace Terminal.Backend.Core;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
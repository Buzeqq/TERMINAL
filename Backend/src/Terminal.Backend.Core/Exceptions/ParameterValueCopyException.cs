using System.Reflection;

namespace Terminal.Backend.Core.Exceptions;

public class ParameterValueCopyException : TerminalException
{
    public ParameterValueCopyException(MemberInfo parameterValueParameter, MemberInfo castedParameter)
        : base($"Cannot cast {castedParameter.Name} to {parameterValueParameter.Name}")
    {
    }
}
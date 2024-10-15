using System.Reflection;

namespace Terminal.Backend.Core.Exceptions;

public class ParameterValueCopyException(MemberInfo parameterValueParameter, MemberInfo castedParameter)
    : TerminalException($"Cannot cast {castedParameter.Name} to {parameterValueParameter.Name}");

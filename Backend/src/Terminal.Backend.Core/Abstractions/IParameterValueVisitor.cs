using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Core.Abstractions;

public interface IParameterValueVisitor<out T>
{
    T Visit(TextParameterValue textParameterValue);
    T Visit(IntegerParameterValue integerParameterValue);
    T Visit(DecimalParameterValue decimalParameterValue);
}

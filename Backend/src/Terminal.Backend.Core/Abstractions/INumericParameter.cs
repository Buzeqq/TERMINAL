namespace Terminal.Backend.Core.Abstractions;

public interface INumericParameter<out T>: IParameter
where T : struct, IComparable<T>
{
    string Unit { get; }
    T Value { get; }
    T StepValue { get; }
}
namespace Terminal.Backend.Core.Abstractions;

public interface IParameter
{
    string Name { get; }
    void Validate();
}
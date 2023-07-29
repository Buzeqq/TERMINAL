namespace Terminal.Backend.Core.Abstractions;

public interface ITextParameter : IParameter
{
    List<string> AllowedValues { get; }
}
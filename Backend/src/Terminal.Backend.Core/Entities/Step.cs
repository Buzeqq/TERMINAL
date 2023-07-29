namespace Terminal.Backend.Core.Entities;

public class Step
{
    public Guid Id { get; private set; }
    public Dictionary<string, string> Paremeters { get; private set; }
}
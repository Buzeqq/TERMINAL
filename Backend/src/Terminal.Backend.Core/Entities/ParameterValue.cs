namespace Terminal.Backend.Core.Entities;

public abstract class ParameterValue
{
    public Guid Id { get; private set; }
    public Parameter Parameter { get; private set; }
    
    protected ParameterValue() { }
}

public sealed class IntegerParameterValue : ParameterValue
{
    public int Value { get; private set; }
}

public sealed class DecimalParameterValue : ParameterValue
{
    public decimal Value { get; private set; }
}

public sealed class TextParameterValue : ParameterValue
{
    public string Value { get; private set; }
}
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Terminal.Backend.Api.Common;

[JsonDerivedType(typeof(TextParameterValue), typeDiscriminator: "text")]
[JsonDerivedType(typeof(IntegerParameterValue), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(DecimalParameterValue), typeDiscriminator: "integer")]
public abstract record ParameterValue(Guid ParameterId);

public sealed record TextParameterValue(Guid ParameterId, string Value) : ParameterValue(ParameterId);
public sealed record IntegerParameterValue(Guid ParameterId, int Value) : ParameterValue(ParameterId);
public sealed record DecimalParameterValue(Guid ParameterId, decimal Value) : ParameterValue(ParameterId);


public static class ParameterValueExtensions
{
    public static T SelectParameterValue<T>(this ParameterValue parameterValue,
        Func<TextParameterValue, T> tm,
        Func<IntegerParameterValue, T> im,
        Func<DecimalParameterValue, T> dm) =>
        parameterValue switch
        {
            TextParameterValue text => tm(text),
            IntegerParameterValue integer => im(integer),
            DecimalParameterValue d => dm(d),
            _ => throw new UnreachableException()
        };

    public static IEnumerable<T> SelectParameterValue<T>(this IEnumerable<ParameterValue> parameterValue,
        Func<TextParameterValue, T> tm,
        Func<IntegerParameterValue, T> im,
        Func<DecimalParameterValue, T> dm) =>
        parameterValue.Select(v => v.SelectParameterValue(tm, im, dm));
}

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) => services;

    public static TResult MapParameter<TResult>(this Parameter parameter,
        Func<TextParameter, TResult> textVisitor,
        Func<IntegerParameter, TResult> intVisitor,
        Func<DecimalParameter, TResult> decimalVisitor) =>
        parameter switch
        {
            TextParameter textParameter => textVisitor(textParameter),
            IntegerParameter intParameter => intVisitor(intParameter),
            DecimalParameter decimalParameter => decimalVisitor(decimalParameter),
            _ => throw new UnreachableException()
        };

    public static TResult MapParameterValue<TResult>(this ParameterValue parameter,
        Func<TextParameterValue, TResult> textVisitor,
        Func<IntegerParameterValue, TResult> intVisitor,
        Func<DecimalParameterValue, TResult> decimalVisitor) =>
        parameter switch
        {
            TextParameterValue textParameter => textVisitor(textParameter),
            IntegerParameterValue intParameter => intVisitor(intParameter),
            DecimalParameterValue decimalParameter => decimalVisitor(decimalParameter),
            _ => throw new UnreachableException()
        };
}

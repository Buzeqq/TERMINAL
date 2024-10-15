using System.Text.Json.Serialization;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.DTO.Parameters;

public record GetParametersDto(IEnumerable<GetParameterDto> Parameters)
{
    public static GetParametersDto Create(IEnumerable<Parameter> parameters) =>
        new(parameters
            .Select<Parameter, GetParameterDto>(p =>
            {
                var parentId = p.ParentId?.Value;
                return p switch
                {
                    IntegerParameter ip => new GetIntegerParameterDto(ip.Id, ip.Name, ip.Unit, ip.Step, ip.Order, ip.DefaultValue, parentId),
                    DecimalParameter dp => new GetDecimalParameterDto(dp.Id,
                        dp.Name,
                        dp.Unit,
                        dp.Step,
                        dp.Order,
                        dp.DefaultValue,
                        parentId),
                    TextParameter tp => new GetTextParameterDto(tp.Id,
                        tp.Name,
                        tp.AllowedValues,
                        tp.Order,
                        tp.DefaultValue,
                        parentId),
                    _ => throw new ArgumentOutOfRangeException(nameof(p), p, "Unknown parameter type.")
                };
            }));
}

[JsonDerivedType(typeof(GetTextParameterDto), "text")]
[JsonDerivedType(typeof(GetDecimalParameterDto), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(GetIntegerParameterDto), typeDiscriminator: "integer")]
public abstract record GetParameterDto(Guid Id, string Name, uint Order, Guid? ParentId = null);

public sealed record GetTextParameterDto(
    Guid Id,
    string Name,
    IEnumerable<string> AllowedValues,
    uint Order,
    uint DefaultValue,
    Guid? ParentId = null)
    : GetParameterDto(Id, Name, Order, ParentId);

public abstract record GetNumericParameterDto(Guid Id, string Name, string Unit, uint Order, Guid? ParentId = null)
    : GetParameterDto(Id, Name, Order, ParentId);

public sealed record GetDecimalParameterDto(
    Guid Id,
    string Name,
    string Unit,
    decimal Step,
    uint Order,
    decimal DefaultValue,
    Guid? ParentId = null)
    : GetNumericParameterDto(Id, Name, Unit, Order, ParentId);

public sealed record GetIntegerParameterDto(
    Guid Id,
    string Name,
    string Unit,
    int Step,
    uint Order,
    int DefaultValue,
    Guid? ParentId = null)
    : GetNumericParameterDto(Id, Name, Unit, Order, ParentId);

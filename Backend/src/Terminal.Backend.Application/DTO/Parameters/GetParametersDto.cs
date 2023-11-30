using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.DTO.Parameters;

public class GetParametersDto
{
    public IEnumerable<GetParameterDto> Parameters { get; set; }
}

[JsonDerivedType(typeof(GetTextParameterDto), "text")]
[JsonDerivedType(typeof(GetDecimalParameterDto), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(GetIntegerParameterDto), typeDiscriminator: "integer")]
public abstract record GetParameterDto(Guid Id, string Name);
public sealed record GetTextParameterDto(Guid Id, string Name, IEnumerable<string> AllowedValues) 
    : GetParameterDto(Id, Name);

public abstract record GetNumericParameterDto(Guid Id, string Name, string Unit) 
    : GetParameterDto(Id, Name);
public sealed record GetDecimalParameterDto(Guid Id, string Name, string Unit, decimal Step) 
    : GetNumericParameterDto(Id, Name, Unit);
public sealed record GetIntegerParameterDto(Guid Id, string Name, string Unit, int Step) 
    : GetNumericParameterDto(Id, Name, Unit);
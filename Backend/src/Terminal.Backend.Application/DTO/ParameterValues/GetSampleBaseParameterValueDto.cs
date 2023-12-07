using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.DTO.ParameterValues;

[JsonDerivedType(typeof(GetSampleTextParameterValueDto), "text")]
[JsonDerivedType(typeof(GetSampleDecimalParameterValueDto), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(GetSampleIntegerParameterValueDto), typeDiscriminator: "integer")]
public abstract record GetSampleBaseParameterValueDto(Guid Id, string Name);
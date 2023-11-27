using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.DTO;

[JsonDerivedType(typeof(CreateSampleTextParameterValueDto), "text")]
[JsonDerivedType(typeof(CreateSampleDecimalParameterValueDto), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(CreateSampleIntegerParameterValueDto), typeDiscriminator: "integer")]
public abstract record CreateSampleBaseParameterValueDto(Guid Id);
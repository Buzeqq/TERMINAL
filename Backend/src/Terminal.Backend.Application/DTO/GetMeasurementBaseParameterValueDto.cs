using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.DTO;

[JsonDerivedType(typeof(GetMeasurementTextParameterValueDto), "text")]
[JsonDerivedType(typeof(GetMeasurementDecimalParameterValueDto), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(GetMeasurementIntegerParameterValueDto), typeDiscriminator: "integer")]
public abstract record GetMeasurementBaseParameterValueDto(string Name);
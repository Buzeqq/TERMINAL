using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.DTO;

[JsonDerivedType(typeof(CreateMeasurementTextParameterValueDto), "text")]
[JsonDerivedType(typeof(CreateMeasurementDecimalParameterValueDto), typeDiscriminator: "decimal")]
[JsonDerivedType(typeof(CreateMeasurementIntegerParameterValueDto), typeDiscriminator: "integer")]
public abstract record CreateMeasurementBaseParameterValueDto(Guid Id);
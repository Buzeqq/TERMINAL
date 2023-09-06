namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementDecimalParameterValueDto(string Name, decimal Value)
    : CreateMeasurementBaseParameterValueDto(Name);
namespace Terminal.Backend.Application.DTO;

public sealed record GetMeasurementDecimalParameterValueDto(string Name, decimal Value, string Unit)
    : GetMeasurementNumericParameterValueDto(Name, Unit);
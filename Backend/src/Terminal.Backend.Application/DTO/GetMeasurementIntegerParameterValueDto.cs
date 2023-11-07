namespace Terminal.Backend.Application.DTO;

public sealed record GetMeasurementIntegerParameterValueDto(string Name, int Value, string Unit)
    : GetMeasurementNumericParameterValueDto(Name, Unit);
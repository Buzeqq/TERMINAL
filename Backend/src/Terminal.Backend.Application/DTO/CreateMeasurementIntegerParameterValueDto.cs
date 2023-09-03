namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementIntegerParameterValueDto(string Name, int Value)
    : CreateMeasurementBaseParameterValueDto(Name);
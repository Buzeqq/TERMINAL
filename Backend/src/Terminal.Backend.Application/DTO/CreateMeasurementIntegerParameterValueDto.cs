namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementIntegerParameterValueDto(Guid Id, int Value)
    : CreateMeasurementBaseParameterValueDto(Id);
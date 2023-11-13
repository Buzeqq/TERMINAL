namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementDecimalParameterValueDto(Guid Id, decimal Value)
    : CreateMeasurementBaseParameterValueDto(Id);
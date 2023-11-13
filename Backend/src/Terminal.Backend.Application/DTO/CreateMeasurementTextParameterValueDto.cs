namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementTextParameterValueDto(Guid Id, string Value) 
    : CreateMeasurementBaseParameterValueDto(Id);
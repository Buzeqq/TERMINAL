namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementTextParameterValueDto(string Name, string Value) 
    : CreateMeasurementBaseParameterValueDto(Name);
namespace Terminal.Backend.Application.DTO;

public sealed record GetMeasurementTextParameterValueDto(string Name, string Value) 
    : GetMeasurementBaseParameterValueDto(Name);
namespace Terminal.Backend.Application.DTO;

public abstract record GetMeasurementNumericParameterValueDto(string Name, string Unit) : GetMeasurementBaseParameterValueDto(Name);
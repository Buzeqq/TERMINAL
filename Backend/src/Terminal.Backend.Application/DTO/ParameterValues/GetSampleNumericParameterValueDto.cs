namespace Terminal.Backend.Application.DTO.ParameterValues;

public abstract record GetSampleNumericParameterValueDto(string Name, string Unit) : GetSampleBaseParameterValueDto(Name);
namespace Terminal.Backend.Application.DTO;

public abstract record GetSampleNumericParameterValueDto(string Name, string Unit) : GetSampleBaseParameterValueDto(Name);
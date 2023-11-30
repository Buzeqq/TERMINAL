namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record GetSampleTextParameterValueDto(string Name, string Value) 
    : GetSampleBaseParameterValueDto(Name);
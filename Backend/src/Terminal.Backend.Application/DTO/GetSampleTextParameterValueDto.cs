namespace Terminal.Backend.Application.DTO;

public sealed record GetSampleTextParameterValueDto(string Name, string Value) 
    : GetSampleBaseParameterValueDto(Name);
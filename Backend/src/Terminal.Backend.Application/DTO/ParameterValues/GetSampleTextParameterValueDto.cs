namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record GetSampleTextParameterValueDto(Guid Id, string Name, string Value)
    : GetSampleBaseParameterValueDto(Id, Name);
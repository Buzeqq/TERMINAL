namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record CreateSampleTextParameterValueDto(Guid Id, string Value)
    : CreateSampleBaseParameterValueDto(Id);
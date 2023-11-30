namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record CreateSampleIntegerParameterValueDto(Guid Id, int Value)
    : CreateSampleBaseParameterValueDto(Id);
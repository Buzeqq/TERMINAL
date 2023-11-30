namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record CreateSampleDecimalParameterValueDto(Guid Id, decimal Value)
    : CreateSampleBaseParameterValueDto(Id);
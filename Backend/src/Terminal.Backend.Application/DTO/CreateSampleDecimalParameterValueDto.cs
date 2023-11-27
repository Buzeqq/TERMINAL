namespace Terminal.Backend.Application.DTO;

public sealed record CreateSampleDecimalParameterValueDto(Guid Id, decimal Value)
    : CreateSampleBaseParameterValueDto(Id);
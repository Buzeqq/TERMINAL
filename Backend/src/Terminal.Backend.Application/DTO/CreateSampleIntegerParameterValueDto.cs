namespace Terminal.Backend.Application.DTO;

public sealed record CreateSampleIntegerParameterValueDto(Guid Id, int Value)
    : CreateSampleBaseParameterValueDto(Id);
namespace Terminal.Backend.Application.DTO;

public sealed record CreateSampleTextParameterValueDto(Guid Id, string Value) 
    : CreateSampleBaseParameterValueDto(Id);
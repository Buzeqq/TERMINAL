namespace Terminal.Backend.Application.DTO;

public sealed record CreateSampleStepDto(
    IEnumerable<CreateSampleBaseParameterValueDto> Parameters,
    string Comment);
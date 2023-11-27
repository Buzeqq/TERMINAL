namespace Terminal.Backend.Application.DTO;

public sealed record GetSampleStepsDto(
    IEnumerable<GetSampleBaseParameterValueDto> Parameters,
    string Comment);
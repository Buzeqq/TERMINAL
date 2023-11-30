using Terminal.Backend.Application.DTO.ParameterValues;

namespace Terminal.Backend.Application.DTO.Samples;

public sealed record CreateSampleStepDto(
    IEnumerable<CreateSampleBaseParameterValueDto> Parameters,
    string Comment);
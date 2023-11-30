using Terminal.Backend.Application.DTO.ParameterValues;

namespace Terminal.Backend.Application.DTO.Samples;

public sealed record GetSampleStepsDto(
    IEnumerable<GetSampleBaseParameterValueDto> Parameters,
    string Comment);
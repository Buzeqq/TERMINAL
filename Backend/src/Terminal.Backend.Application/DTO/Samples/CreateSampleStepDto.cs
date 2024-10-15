using Terminal.Backend.Application.DTO.ParameterValues;

namespace Terminal.Backend.Application.DTO.Samples;

public sealed record CreateSampleStepDto(
    IEnumerable<StepParameterValueDto> Parameters,
    string Comment);

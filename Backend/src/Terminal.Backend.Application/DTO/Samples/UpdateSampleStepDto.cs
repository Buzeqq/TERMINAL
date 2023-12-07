using Terminal.Backend.Application.DTO.ParameterValues;

namespace Terminal.Backend.Application.DTO.Samples;

public sealed record UpdateSampleStepDto(
    Guid Id,
    IEnumerable<CreateSampleBaseParameterValueDto> Parameters,
    string Comment);
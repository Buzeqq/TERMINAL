namespace Terminal.Backend.Application.DTO;

public sealed record GetMeasurementStepsDto(
    IEnumerable<GetMeasurementBaseParameterValueDto> Parameters,
    string Comment);
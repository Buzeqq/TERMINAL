namespace Terminal.Backend.Application.DTO;

public sealed record CreateMeasurementStepDto(
    IEnumerable<CreateMeasurementBaseParameterValueDto> Parameters,
    string Comment);
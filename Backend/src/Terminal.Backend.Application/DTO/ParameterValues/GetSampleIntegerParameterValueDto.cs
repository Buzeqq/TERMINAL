namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record GetSampleIntegerParameterValueDto(Guid Id, string Name, int Value, string Unit)
    : GetSampleNumericParameterValueDto(Id, Name, Unit);
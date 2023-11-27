namespace Terminal.Backend.Application.DTO;

public sealed record GetSampleIntegerParameterValueDto(string Name, int Value, string Unit)
    : GetSampleNumericParameterValueDto(Name, Unit);
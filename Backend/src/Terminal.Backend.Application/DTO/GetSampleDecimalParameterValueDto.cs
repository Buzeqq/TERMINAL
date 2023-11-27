namespace Terminal.Backend.Application.DTO;

public sealed record GetSampleDecimalParameterValueDto(string Name, decimal Value, string Unit)
    : GetSampleNumericParameterValueDto(Name, Unit);
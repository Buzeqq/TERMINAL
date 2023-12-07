namespace Terminal.Backend.Application.DTO.ParameterValues;

public abstract record GetSampleNumericParameterValueDto(Guid Id, string Name, string Unit)
    : GetSampleBaseParameterValueDto(Id, Name);
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Application.Common;

internal class ParameterValueToDtoVisitor : IParameterValueVisitor<GetSampleBaseParameterValueDto>
{
    public GetSampleBaseParameterValueDto Visit(TextParameterValue textParameterValue) =>
        new GetSampleTextParameterValueDto(
            textParameterValue.Id,
            textParameterValue.Parameter.Name,
            textParameterValue.Value);

    public GetSampleBaseParameterValueDto Visit(IntegerParameterValue integerParameterValue) =>
        new GetSampleIntegerParameterValueDto(
            integerParameterValue.Id,
            integerParameterValue.Parameter.Name,
            integerParameterValue.Value,
            integerParameterValue.IntegerParameter.Unit);

    public GetSampleBaseParameterValueDto Visit(DecimalParameterValue decimalParameterValue) =>
        new GetSampleDecimalParameterValueDto(
            decimalParameterValue.Id,
            decimalParameterValue.Parameter.Name,
            decimalParameterValue.Value,
            decimalParameterValue.DecimalParameter.Unit);
}

using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Services;

public interface IConvertDtoService
{
    Task<IEnumerable<Step>> ConvertAsync(IEnumerable<CreateMeasurementStepDto> stepsDto, CancellationToken ct);
    Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<TagName> tagNames, CancellationToken ct);
}
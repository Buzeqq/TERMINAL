using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Application.Services;

public interface IConvertDtoService
{
    Task<IEnumerable<Step>> ConvertAsync(IEnumerable<CreateMeasurementStepDto> stepsDto, CancellationToken ct);
    Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<string> tagNames, CancellationToken ct);
}
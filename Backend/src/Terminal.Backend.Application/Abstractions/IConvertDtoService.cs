using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Abstractions;

internal interface IConvertDtoService
{
    Task<IEnumerable<Step>> ConvertAsync(IEnumerable<CreateSampleStepDto> stepsDto, CancellationToken ct);
    Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<TagId> tagIds, CancellationToken ct);
}
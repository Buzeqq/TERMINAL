using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Common.QueryParameters;

namespace Terminal.Backend.Application.DTO.Samples;

public record GetSamplesDto(IEnumerable<GetSamplesDto.SampleDto> Samples, int TotalCount, PagingParameters PagingParameters)
    : PaginatedResult(TotalCount, PagingParameters)
{
    public sealed record SampleDto(Guid Id, string Code, string ProjectName, string? RecipeName, string CreatedAtUtc, string Comment);
}

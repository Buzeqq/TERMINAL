using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Common.QueryParameters;

namespace Terminal.Backend.Application.DTO.Tags;

public sealed record GetTagsDto(IEnumerable<GetTagsDto.TagDto> Tags, int TotalCount, PagingParameters PagingParameters)
    : PaginatedResult(TotalCount, PagingParameters)
{
    public record TagDto(Guid Id, string Name);
}

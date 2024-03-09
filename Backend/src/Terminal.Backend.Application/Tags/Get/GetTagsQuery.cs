using MediatR;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.Tags.Get;

public sealed class GetTagsQuery(int pageNumber, int pageSize, bool desc, bool onlyActive = true)
    : IRequest<GetTagsDto>
{
    public PagingParameters Parameters { get; set; } = new() { PageNumber = pageNumber, PageSize = pageSize };

    public OrderingParameters OrderingParameters { get; set; } = new() { OrderBy = "Name", Desc = desc };

    public bool OnlyActive { get; set; } = onlyActive;
}
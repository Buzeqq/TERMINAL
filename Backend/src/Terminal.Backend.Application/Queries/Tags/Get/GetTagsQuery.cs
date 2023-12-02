using MediatR;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Tags.Get;

public sealed class GetTagsQuery : IRequest<GetTagsDto>
{
    public PagingParameters Parameters { get; set; }
    
    public OrderingParameters OrderingParameters { get; set; }

    public GetTagsQuery(int pageNumber, int pageSize, bool desc)
    {
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
        OrderingParameters = new OrderingParameters { OrderBy = "Name", Desc = desc };
    }
}
using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters;

namespace Terminal.Backend.Application.Queries;

public sealed class GetTagsQuery : IRequest<GetTagsDto>
{
    public PagingParameters Parameters { get; set; }

    public GetTagsQuery(int pageNumber, int pageSize)
    {
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
    }
}
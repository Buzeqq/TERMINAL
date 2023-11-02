using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters;

namespace Terminal.Backend.Application.Queries;

public sealed class GetProjectsQuery : IRequest<GetProjectsDto>
{
    public PagingParameters Parameters { get; set; }

    public GetProjectsQuery(int pageNumber, int pageSize)
    {
        Parameters = new PagingParameters { PageSize = pageSize, PageNumber = pageNumber };
    }
}
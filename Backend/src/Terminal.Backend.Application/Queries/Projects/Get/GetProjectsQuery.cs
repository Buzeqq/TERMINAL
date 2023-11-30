using MediatR;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Projects.Get;

public sealed class GetProjectsQuery : IRequest<GetProjectsDto>
{
    public PagingParameters Parameters { get; set; }

    public GetProjectsQuery(int pageNumber, int pageSize)
    {
        Parameters = new PagingParameters { PageSize = pageSize, PageNumber = pageNumber };
    }
}
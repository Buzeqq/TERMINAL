using MediatR;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Projects.Get;

public sealed class GetProjectsQuery : IRequest<GetProjectsDto>
{
    public PagingParameters Parameters { get; set; }

    public OrderingParameters OrderingParameters { get; set; }

    public bool OnlyActive { get; set; }

    public GetProjectsQuery(int pageNumber, int pageSize, bool desc, bool onlyActive = true)
    {
        Parameters = new PagingParameters { PageSize = pageSize, PageNumber = pageNumber };
        OrderingParameters = new OrderingParameters { OrderBy = "Name", Desc = desc };
        OnlyActive = onlyActive;
    }
}
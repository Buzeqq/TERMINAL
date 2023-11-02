using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters;

namespace Terminal.Backend.Application.Queries;

public class SearchProjectQuery : IRequest<GetProjectsDto>
{
    public string SearchPhrase { get; set; }
    public PagingParameters Parameters { get; set; }
    public SearchProjectQuery(string searchPhrase, int pageNumber, int pageSize)
    {
        SearchPhrase = searchPhrase;
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
    }
}
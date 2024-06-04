using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Projects;

namespace Terminal.Backend.Application.Projects.Search;

public class SearchProjectQuery(string searchPhrase, int pageNumber, int pageSize) : IRequest<GetProjectsDto>
{
    public string SearchPhrase { get; set; } = searchPhrase;
    public PagingParameters Parameters { get; set; } = new() { PageNumber = pageNumber, PageSize = pageSize };
}
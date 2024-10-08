using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Projects;

namespace Terminal.Backend.Application.Projects.Get;

public sealed class GetProjectsQuery(int pageNumber, int pageSize, bool desc, string? searchPhrase, bool onlyActive = true)
    : IRequest<GetProjectsDto>
{
    public PagingParameters Parameters { get; set; } = new() { PageSize = pageSize, PageNumber = pageNumber };

    public OrderingParameters OrderingParameters { get; set; } = new() { OrderBy = "Name", Desc = desc };
    public string? SearchPhrase { get; set; } = searchPhrase;
    public bool OnlyActive { get; set; } = onlyActive;
}

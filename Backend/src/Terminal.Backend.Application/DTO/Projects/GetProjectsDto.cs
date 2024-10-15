using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Application.DTO.Projects;

public record GetProjectsDto(
    IEnumerable<GetProjectsDto.ProjectDto> Projects,
    int TotalCount,
    PagingParameters PagingParameters) : PaginatedResult(TotalCount,
    PagingParameters)
{
    public record ProjectDto(Guid Id, string Name);

    public static GetProjectsDto Create(IEnumerable<Project> projects, int totalCount, PagingParameters pagingParameters)
        => new GetProjectsDto(projects.Select(p => new ProjectDto(p.Id, p.Name)), totalCount, pagingParameters);
}

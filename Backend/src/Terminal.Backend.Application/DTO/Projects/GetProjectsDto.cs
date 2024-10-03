using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.DTO.Projects;

public record GetProjectsDto(
    IEnumerable<GetProjectsDto.ProjectDto> Projects,
    int TotalCount,
    int PageIndex,
    int PageSize) : PaginatedResult<GetProjectsDto.ProjectDto>(Projects,
    TotalCount,
    PageIndex,
    PageSize)
{
    public record ProjectDto(Guid Id, string Name);
}

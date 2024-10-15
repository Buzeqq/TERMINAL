using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Projects;

namespace Terminal.Backend.Application.Projects.Get;

public record GetProjectsQuery(
    string? SearchPhrase,
    PagingParameters PagingParameters,
    OrderingParameters OrderingParameters)
    : IRequest<GetProjectsDto>;

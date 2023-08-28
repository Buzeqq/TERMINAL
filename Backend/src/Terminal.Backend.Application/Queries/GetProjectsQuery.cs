using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public sealed class GetProjectsQuery : IQuery<IEnumerable<GetProjectsDto>>
{
}
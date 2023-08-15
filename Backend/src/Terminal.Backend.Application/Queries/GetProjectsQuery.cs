using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public class GetProjectsQuery : IRequest<IEnumerable<GetProjectsDto>>
{
    
}
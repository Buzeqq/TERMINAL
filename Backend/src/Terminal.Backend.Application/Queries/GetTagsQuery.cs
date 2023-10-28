using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public sealed class GetTagsQuery : IRequest<GetTagsDto>
{
}
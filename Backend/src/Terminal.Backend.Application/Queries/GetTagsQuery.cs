using MediatR;

namespace Terminal.Backend.Application.Queries;

public sealed class GetTagsQuery : IRequest<IEnumerable<string>>
{
}
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Queries;

public sealed class GetTagsQuery : IQuery<IEnumerable<string>>
{
}
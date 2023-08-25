using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public sealed class GetMostPopularTagsQuery : IQuery<IEnumerable<GetTagsDto>>
{
    // TODO: read from config file max count
    public int Count { get; set; }
}
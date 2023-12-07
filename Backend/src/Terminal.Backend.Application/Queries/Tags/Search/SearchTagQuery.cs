using MediatR;
using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.Queries.Tags.Search;

public class SearchTagQuery : IRequest<GetTagsDto>
{
    public string SearchPhrase { get; set; }

    public SearchTagQuery(string searchPhrase)
    {
        SearchPhrase = searchPhrase;
    }
}
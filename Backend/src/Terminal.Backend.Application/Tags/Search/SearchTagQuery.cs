using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.Tags.Search;

public class SearchTagQuery(string searchPhrase) : IRequest<GetTagsDto>
{
    public string SearchPhrase { get; set; } = searchPhrase;
}
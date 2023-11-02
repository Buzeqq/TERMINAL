using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public class SearchTagQuery : IRequest<GetTagsDto>
{
    public string SearchPhrase { get; set; }
    public SearchTagQuery(string searchPhrase)
    {
        SearchPhrase = searchPhrase;
    }
}
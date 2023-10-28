using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public class SearchRecipeQuery : IRequest<GetRecipesDto>
{
    public string SearchPhrase { get; set; }
    
    public SearchRecipeQuery(string searchPhrase)
    {
        SearchPhrase = searchPhrase;
    }
}
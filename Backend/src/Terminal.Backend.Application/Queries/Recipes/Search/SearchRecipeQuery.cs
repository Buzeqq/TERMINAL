using MediatR;
using Terminal.Backend.Application.DTO.Recipes;

namespace Terminal.Backend.Application.Queries.Recipes.Search;

public class SearchRecipeQuery : IRequest<GetRecipesDto>
{
    public string SearchPhrase { get; set; }
    
    public SearchRecipeQuery(string searchPhrase)
    {
        SearchPhrase = searchPhrase;
    }
}
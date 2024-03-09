using MediatR;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Recipes;

namespace Terminal.Backend.Application.Recipes.Search;

public class SearchRecipeQuery(string searchPhrase, int pageNumber, int pageSize) : IRequest<GetRecipesDto>
{
    public string SearchPhrase { get; set; } = searchPhrase;
    public PagingParameters Parameters { get; set; } = new() { PageNumber = pageNumber, PageSize = pageSize };
}
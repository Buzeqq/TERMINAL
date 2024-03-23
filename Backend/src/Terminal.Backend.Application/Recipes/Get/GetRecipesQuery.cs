using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Recipes;

namespace Terminal.Backend.Application.Recipes.Get;

public sealed class GetRecipesQuery(int pageNumber, int pageSize, bool desc) : IRequest<GetRecipesDto>
{
    public PagingParameters Parameters { get; set; } = new() { PageSize = pageSize, PageNumber = pageNumber };

    public OrderingParameters OrderingParameters { get; set; } = new() { OrderBy = "RecipeName", Desc = desc };
}
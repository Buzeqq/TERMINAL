using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Application.DTO.Recipes;

public record GetRecipesDto(
    IEnumerable<GetRecipesDto.RecipeDto> Recipes,
    int TotalCount,
    PagingParameters PagingParameters) : PaginatedResult(TotalCount, PagingParameters)
{
    public record RecipeDto(Guid Id, string Name);

    public static GetRecipesDto Create(IEnumerable<Recipe> recipes, int totalCount, PagingParameters pagingParameters)
        => new GetRecipesDto(recipes.Select(r => new RecipeDto(r.Id, r.Name)), totalCount, pagingParameters);
}

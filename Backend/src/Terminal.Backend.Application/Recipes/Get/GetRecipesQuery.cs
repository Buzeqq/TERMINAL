using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Recipes;

namespace Terminal.Backend.Application.Recipes.Get;

public record GetRecipesQuery(
    string? SearchPhrase,
    PagingParameters PagingParameters,
    OrderingParameters OrderingParameters) : IRequest<GetRecipesDto>;

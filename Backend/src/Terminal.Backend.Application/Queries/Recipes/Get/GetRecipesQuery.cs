using MediatR;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Recipes.Get;

public sealed record GetRecipesQuery(PagingParameters Parameters) : IRequest<GetRecipesDto>;
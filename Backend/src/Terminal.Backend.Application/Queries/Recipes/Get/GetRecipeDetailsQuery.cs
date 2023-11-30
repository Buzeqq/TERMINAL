using MediatR;
using Terminal.Backend.Application.DTO.Recipes;

namespace Terminal.Backend.Application.Queries.Recipes.Get;

public sealed record GetRecipeDetailsQuery(Guid Id) : IRequest<GetRecipeDetailsDto?>;
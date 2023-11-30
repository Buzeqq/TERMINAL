using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries.Recipes;

public sealed record GetRecipeDetailsQuery(Guid Id) : IRequest<GetRecipeDetailsDto?>;
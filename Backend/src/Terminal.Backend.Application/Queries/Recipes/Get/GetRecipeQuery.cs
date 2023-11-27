using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries.Recipes.Get;

public sealed record GetRecipeQuery(string Name) : IRequest<GetRecipeDto?>;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Get;

public record GetRecipeQuery(RecipeName Name) : IRequest<GetRecipeDto?>;

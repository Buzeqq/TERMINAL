using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Create;

public record CreateRecipeCommand(
    RecipeId Id,
    RecipeName Name,
    IEnumerable<CreateSampleStepDto> Steps) : IRequest;

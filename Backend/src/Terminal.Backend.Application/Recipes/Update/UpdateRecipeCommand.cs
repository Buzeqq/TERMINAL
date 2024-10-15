using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Update;

public record UpdateRecipeCommand(
    RecipeId Id,
    RecipeName Name,
    IEnumerable<UpdateSampleStepDto> Steps) : IRequest;

using System.Text.Json.Serialization;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Create;

public sealed record CreateRecipeCommand(
    [property: JsonIgnore] RecipeId Id,
    string Name,
    IEnumerable<CreateSampleStepDto> Steps) : IRequest;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Api.Recipes.Requests;

public record UpdateRecipeRequest(string Name, IEnumerable<UpdateSampleStepDto> Steps);

using Terminal.Backend.Api.Common;

namespace Terminal.Backend.Api.Recipes.Requests;

public record CreateRecipeRequest(string Name, SampleStep[] Steps);

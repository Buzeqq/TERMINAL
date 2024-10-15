using Terminal.Backend.Api.Common;

namespace Terminal.Backend.Api.Samples.Requests;

public record CreateSampleRequest(Guid ProjectId, Guid? RecipeId, SampleStep[] Steps, Guid[] Tags, string Comment, bool SaveAsARecipe, string? RecipeName);

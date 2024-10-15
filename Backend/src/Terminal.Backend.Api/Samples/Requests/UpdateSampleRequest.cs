using Terminal.Backend.Api.Common;

namespace Terminal.Backend.Api.Samples.Requests;

public record UpdateSampleRequest(Guid ProjectId, Guid? RecipeId, SampleStep[] Steps, Guid[] Tags, string Comment);

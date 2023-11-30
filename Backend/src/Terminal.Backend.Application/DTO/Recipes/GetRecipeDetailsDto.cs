using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.DTO.Recipes;

public sealed record GetRecipeDetailsDto(Guid Id, string Name, IEnumerable<GetSampleStepsDto> Steps);
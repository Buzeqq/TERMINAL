namespace Terminal.Backend.Application.DTO;

public sealed record GetRecipeDetailsDto(Guid Id, string Name, IEnumerable<GetSampleStepsDto> Steps);
namespace Terminal.Backend.Application.DTO.Tags;

public sealed record GetTagDto(Guid Id, string Name, bool IsActive);
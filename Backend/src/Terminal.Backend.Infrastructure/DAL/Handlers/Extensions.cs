using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static GetProjectsDto AsGetProjectsDto(this IEnumerable<Project> entities)
        => new()
        {
            Projects = entities.Select(p => new GetProjectsDto.InnerProjectDto(p.Id, p.Name))
        };

    public static GetProjectDto AsGetProjectDto(this Project entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
            MeasurementsIds = entity.Measurements.Select(m => m.Id.Value)
        };

    public static GetMeasurementDto AsGetMeasurementDto(this Measurement entity)
        => new()
        {
            Id = entity.Id,
            ProjectId = entity.Project.Id,
            RecipeId = entity.Recipe?.Id.Value,
            Code = entity.Code.Value,
            Comment = entity.Comment.Value,
            CreatedAtUtc = entity.CreatedAtUtc.ToString("o"),
            StepIds = entity.Steps.Select(s => s.Id.Value),
            Tags = entity.Tags.Select(t => t.Name.Value),
        };
}
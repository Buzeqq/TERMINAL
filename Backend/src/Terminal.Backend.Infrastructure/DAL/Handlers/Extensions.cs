using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static GetProjectsDto AsGetProjectsDto(this IEnumerable<Project> entities)
        => new()
        {
            Projects = entities.Select(p => new GetProjectsDto.ProjectDto(p.Id, p.Name))
        };

    public static GetProjectDto AsGetProjectDto(this Project entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
            MeasurementsIds = entity.Measurements.Select(m => m.Id.Value)
        };

    // public static GetMeasurementDto AsGetMeasurementDto(this Measurement entity)
    //     => new()
    //     {
    //         Id = entity.Id,
    //         ProjectId = entity.Project.Id,
    //         RecipeId = entity.Recipe?.Id.Value,
    //         Code = entity.Code.Value,
    //         Comment = entity.Comment.Value,
    //         CreatedAtUtc = entity.CreatedAtUtc.ToString("o"),
    //         Steps = entity.Steps.Select(s => s.Id),
    //         Tags = entity.Tags.Select(t => t.Name.Value)
    //     };

    public static IEnumerable<CreateMeasurementStepDto> AsStepsDto(this IEnumerable<Step> steps)
        => steps.Select(s => new CreateMeasurementStepDto(
            s.Parameters.Select(p =>
            {
                CreateMeasurementBaseParameterValueDto b = p switch
                {
                    DecimalParameterValue d => new CreateMeasurementDecimalParameterValueDto(d.Parameter.Name, d.Value),
                    IntegerParameterValue i => new CreateMeasurementIntegerParameterValueDto(i.Parameter.Name, i.Value),
                    TextParameterValue t => new CreateMeasurementTextParameterValueDto(t.Parameter.Name, t.Value),
                    _ => throw new ArgumentOutOfRangeException(nameof(p))
                };
                return b;
            }), s.Comment));

    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PagingParameters parameters)
        => queryable.Skip(parameters.PageNumber * parameters.PageSize).Take(parameters.PageSize);
}
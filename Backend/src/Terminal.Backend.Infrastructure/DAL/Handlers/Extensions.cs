using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.QueryParameters;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;

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
            SamplesIds = entity.Samples.Select(m => m.Id.Value)
        };

    public static GetTagsDto AsGetTagsDto(this IEnumerable<Tag> entities)
        => new()
        {
            Tags = entities.Select(t => new GetTagsDto.TagDto(t.Id, t.Name))
        };
    
    public static GetTagDto AsGetTagDto(this Tag entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive
        };
    
    // public static GetSampleDto AsGetSampleDto(this Sample entity)
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

    public static IEnumerable<GetSampleStepsDto> AsStepsDto(this IEnumerable<Step> steps)
        => steps.Select(s => new GetSampleStepsDto(
            s.Parameters.Select(p =>
            {
                GetSampleBaseParameterValueDto b = p switch
                {
                    DecimalParameterValue d => new GetSampleDecimalParameterValueDto(d.Parameter.Name, d.Value, (d.Parameter as DecimalParameter)!.Unit),
                    IntegerParameterValue i => new GetSampleIntegerParameterValueDto(i.Parameter.Name, i.Value, (i.Parameter as IntegerParameter)!.Unit),
                    TextParameterValue t => new GetSampleTextParameterValueDto(t.Parameter.Name, t.Value),
                    _ => throw new ArgumentOutOfRangeException(nameof(p))
                };
                return b;
            }), s.Comment));

    public static GetParametersDto AsGetParametersDto(this IEnumerable<Parameter> parameters)
    {
        return new GetParametersDto
        {
            Parameters = parameters.Select(MapParameters)
        };
    }

    private static GetParameterDto MapParameters(Parameter parameter)
    {
        return parameter switch
        {
            IntegerParameter i => new GetIntegerParameterDto(i.Id, i.Name, i.Unit, i.Step),
            DecimalParameter d => new GetDecimalParameterDto(d.Id, d.Name, d.Unit, d.Step),
            TextParameter t => new GetTextParameterDto(t.Id, t.Name, t.AllowedValues),
            _ => throw new ArgumentOutOfRangeException(nameof(parameter))
        };
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PagingParameters parameters)
        => queryable.Skip(parameters.PageNumber * parameters.PageSize).Take(parameters.PageSize);

    public static GetRecipeDto AsDto(this Recipe recipe)
    {
        return new GetRecipeDto
        {
            Id = recipe.Id,
            Name = recipe.RecipeName,
            Steps = recipe.Steps.AsStepsDto()
        };
    }
}
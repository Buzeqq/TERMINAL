using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Samples.Get;
using Terminal.Backend.Core.Abstractions;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Samples;

internal class GetSampleQueryHandler(
    TerminalDbContext dbContext,
    IParameterValueVisitor<GetSampleBaseParameterValueDto> visitor) : IRequestHandler<GetSampleQuery, GetSampleDto?>
{
    public async Task<GetSampleDto?> Handle(GetSampleQuery request, CancellationToken cancellationToken)
    {
        var sampleData = await dbContext.Samples
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new
            {
                s.Id,
                Code = s.Code.Value,
                s.Comment,
                s.CreatedAtUtc,
                ProjectId = s.Project.Id,
                Recipe = s.Recipe != null ? new { s.Recipe.Id, s.Recipe.Name } : null,
                Steps = s.Steps.Select(st => new
                {
                    st.Id,
                    Values = st.Values.Select(pv => new
                    {
                        ParameterValue = pv,
                    }),
                    s.Comment
                }),
                Tags = s.Tags.Select(t => new
                {
                    t.Id,
                    t.Name,
                })
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (sampleData is null)
        {
            return null;
        }

        return new GetSampleDto
        {
            Id = sampleData.Id,
            Code = sampleData.Code,
            Comment = sampleData.Comment,
            CreatedAtUtc = sampleData.CreatedAtUtc.ToString("o"),
            ProjectId = sampleData.ProjectId,
            Recipe = sampleData.Recipe is null ? null : new GetRecipeDto(sampleData.Recipe.Id, sampleData.Recipe.Name),
            Steps = sampleData.Steps.Select(s => new GetSampleStepsDto(
                s.Id,
                s.Values.Select(v => v.ParameterValue.Accept(visitor)),
                s.Comment)),
            Tags = sampleData.Tags.Select(t => new GetTagsDto.TagDto(t.Id, t.Name))
        };
    }
}

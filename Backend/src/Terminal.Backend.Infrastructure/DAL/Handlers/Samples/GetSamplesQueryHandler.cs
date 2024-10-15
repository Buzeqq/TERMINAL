using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Samples.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Samples;

internal sealed class GetSamplesQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetSamplesQuery, GetSamplesDto>
{
    private readonly DbSet<Sample> _samples = dbContext.Samples;

    public async Task<GetSamplesDto> Handle(GetSamplesQuery request, CancellationToken cancellationToken)
    {
        var samplesQuery = _samples
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(s => s.Project.IsActive)
            .Select(s => new
            {
                s.Id,
                Code = s.Code.Value,
                ProjectName = s.Project.Name,
                RecipeName = s.Recipe != null ? s.Recipe.Name : null,
                s.CreatedAtUtc,
                s.Comment
            });

        if (!string.IsNullOrWhiteSpace(request.SearchPhrase))
        {
            samplesQuery = samplesQuery
                .Where(s =>
                    s.ProjectName.Value.StartsWith(request.SearchPhrase) ||
                    (s.RecipeName != null && s.RecipeName.Value.Contains(request.SearchPhrase)) ||
                    EF.Functions.ToTsVector("english", "AX" + s.Code + " " + s.Comment)
                        .Matches(EF.Functions.PhraseToTsQuery($"{request.SearchPhrase}:*"))
                );
        }

        samplesQuery = samplesQuery.OrderBy(request.OrderingParameters);

        var totalCount = await samplesQuery.CountAsync(cancellationToken);

        var samples = await samplesQuery
            .Select(m => new GetSamplesDto.SampleDto(
                m.Id,
                m.Code,
                m.ProjectName,
                m.RecipeName != null ? m.RecipeName.Value : null,
                m.CreatedAtUtc.ToString("o"),
                m.Comment))
            .Paginate(request.PagingParameters)
            .ToListAsync(cancellationToken);

        return new GetSamplesDto(samples, totalCount, request.PagingParameters);
    }
}

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

    public async Task<GetSamplesDto> Handle(GetSamplesQuery request, CancellationToken ct)
    {
        IQueryable<Sample> samplesQuery = _samples
            .AsNoTracking()
            .Include(s => s.Project)
            .Include(s => s.Tags);

        var shouldSearch = !string.IsNullOrWhiteSpace(request.SearchPhrase);
        if (shouldSearch)
        {
            samplesQuery = samplesQuery
                .Where(s =>
                    EF.Functions.ToTsVector("english", "AX" + s.Code + " " + s.Comment)
                        .Matches(EF.Functions.PhraseToTsQuery($"{request.SearchPhrase}:*")) ||
                    EF.Functions.ILike(s.Project.Name, $"%{request.SearchPhrase}%") ||
                    EF.Functions.ILike(s.Recipe!.RecipeName, $"%{request.SearchPhrase}%"));
        }

        var totalCount = await samplesQuery.CountAsync(ct);
        var samples = await samplesQuery
            .OrderBy(request.OrderingParameters)
            .Paginate(request.Parameters)
            .Select(m => new GetSamplesDto.SampleDto(
                m.Id, m.Code.Value, m.Project.Name, m.CreatedAtUtc.ToString("o"), m.Comment))
            .ToListAsync(ct);

        return new GetSamplesDto { Samples = samples, TotalCount = totalCount };
    }
}

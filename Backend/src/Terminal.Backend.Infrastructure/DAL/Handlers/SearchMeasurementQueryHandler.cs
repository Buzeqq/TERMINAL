using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Samples.Search;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class SearchMeasurementQueryHandler : IRequestHandler<SearchSampleQuery, GetSamplesDto>
{
    private readonly DbSet<Sample> _measurements;

    public SearchMeasurementQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task<GetSamplesDto> Handle(SearchSampleQuery request, CancellationToken ct)
        => new()
        {
            Samples = await _measurements
                .AsNoTracking()
                .Include(m => m.Project)
                .Where(m => 
                    EF.Functions.ToTsVector("english", "AX" + m.Code + " " + m.Comment).Matches(request.SearchPhrase) || 
                    EF.Functions.ILike(m.Project.Name, $"%{request.SearchPhrase}%"))
                .Select(m => new GetSamplesDto.SampleDto(m.Id, m.Code.Value, m.Project.Name, m.CreatedAtUtc.ToString("o"), m.Comment))
                .Paginate(request.Parameters)
                .ToListAsync(ct)
        };
}
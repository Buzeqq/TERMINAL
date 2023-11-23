using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Measurements.Search;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class SearchMeasurementQueryHandler : IRequestHandler<SearchMeasurementQuery, GetMeasurementsDto>
{
    private readonly DbSet<Measurement> _measurements;

    public SearchMeasurementQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task<GetMeasurementsDto> Handle(SearchMeasurementQuery request, CancellationToken ct)
        => new()
        {
            Measurements = await _measurements
                .AsNoTracking()
                .Include(m => m.Project)
                .Where(m => 
                    EF.Functions.ToTsVector("english", "AX" + m.Code + " " + m.Comment).Matches(request.SearchPhrase) || 
                    EF.Functions.ILike(m.Project.Name, $"%{request.SearchPhrase}%"))
                .Select(m => new GetMeasurementsDto.MeasurementDto(m.Id, m.Code.Value, m.Project.Name, m.CreatedAtUtc.ToString("o"), m.Comment))
                .Paginate(request.Parameters)
                .ToListAsync(ct)
        };
}
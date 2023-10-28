using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetRecentMeasurementsQueryHandler : 
    IRequestHandler<GetRecentMeasurementsQuery, GetRecentMeasurementsDto>
{
    private readonly DbSet<Measurement> _measurements;

    public GetRecentMeasurementsQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task<GetRecentMeasurementsDto> Handle(GetRecentMeasurementsQuery request,
        CancellationToken cancellationToken)
        => new()
        {
            RecentMeasurements = await _measurements
                .Take(request.Length)
                .OrderByDescending(m => m.CreatedAtUtc)
                .Select(m => new GetMeasurementsDto.MeasurementDto(m.Id, m.Code.Value, m.Project.Name, m.CreatedAtUtc.ToString("o")))
                .ToListAsync(cancellationToken)
        };
}
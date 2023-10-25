using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;
internal class GetMeasurementQueryHandler : IRequestHandler<GetMeasurementQuery, GetMeasurementDto?>
{
    private readonly DbSet<Measurement> _measurements;

    public GetMeasurementQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task<GetMeasurementDto?> Handle(GetMeasurementQuery request, CancellationToken ct)
    {
        var measurement = await _measurements
            .AsNoTracking()
            .Include(m => m.Project)
            .Include(m => m.Recipe)
            .Include(m => m.Tags)
            .AsSplitQuery()
            .SingleOrDefaultAsync(m => m.Id == request.Id, ct);
        
        
        return measurement?.AsGetMeasurementDto();
    }
}
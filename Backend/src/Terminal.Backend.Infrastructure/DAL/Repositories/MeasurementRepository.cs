using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class MeasurementRepository : IMeasurementRepository
{
    private readonly DbSet<Measurement> _measurements;

    public MeasurementRepository(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task AddAsync(Measurement measurement, CancellationToken ct)
        => await _measurements.AddAsync(measurement, ct);
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.ValueGenerators;

internal sealed class MeasurementCodeValueGenerator : ValueGenerator<MeasurementCode>
{
    public override bool GeneratesTemporaryValues => false;

    private readonly TerminalDbContext _dbContext;
    private const ulong InitialNumberValue = 1;
    
    public MeasurementCodeValueGenerator(TerminalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public override MeasurementCode Next(EntityEntry entry)
    {
        var lastCodeNumber = _dbContext.Measurements
            .Include(measurement => measurement.Code)
            .LastOrDefault()
            ?.Code.Number;

        return lastCodeNumber is null ? new MeasurementCode(InitialNumberValue) : new MeasurementCode((ulong)(lastCodeNumber + 1));
    }

    public override async ValueTask<MeasurementCode> NextAsync(EntityEntry entry, CancellationToken cancellationToken = default)
    {
        var lastCodeNumber = (await _dbContext.Measurements
            .Include(measurement => measurement.Code)
            .LastOrDefaultAsync(cancellationToken))
            ?.Code.Number;
        
        return lastCodeNumber is null ? new MeasurementCode(InitialNumberValue) : new MeasurementCode((ulong)(lastCodeNumber + 1));
    }
}
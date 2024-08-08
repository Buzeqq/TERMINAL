using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.ValueGenerators;

internal sealed class SampleCodeValueGenerator : ValueGenerator<SampleCode>
{
    public override bool GeneratesTemporaryValues => false;

    private const ulong InitialNumberValue = 1;
    private ulong _currentNumberValue = InitialNumberValue;

    public override SampleCode Next(EntityEntry entry)
    {
        var dbContext = entry.Context as TerminalDbContext ?? throw new InvalidDataException();

        _currentNumberValue = dbContext.Samples
            .OrderBy(m => m.Code)
            .Select(m => m.Code)
            .LastOrDefault()?.Number ?? _currentNumberValue;

        return new SampleCode(_currentNumberValue++);
    }

    public override async ValueTask<SampleCode> NextAsync(EntityEntry entry, CancellationToken ct = default)
    {
        var dbContext = entry.Context as TerminalDbContext ?? throw new InvalidDataException();
        _currentNumberValue = (await dbContext.Samples.OrderBy(m => m.Code).LastOrDefaultAsync(ct))
            ?.Code.Number ?? _currentNumberValue;

        return new SampleCode(_currentNumberValue++);

    }
}

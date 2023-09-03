using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ParameterValueRepository : IParameterValueRepository
{
    private readonly DbSet<ParameterValue> _values;

    public ParameterValueRepository(TerminalDbContext dbContext)
    {
        _values = dbContext.ParameterValues;
    }

    public async Task AddAsync(ParameterValue value, CancellationToken ct)
        => await _values.AddAsync(value, ct);
}   
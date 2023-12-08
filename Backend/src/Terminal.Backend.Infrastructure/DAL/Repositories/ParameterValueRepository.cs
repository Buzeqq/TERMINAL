using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities.ParameterValues;

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
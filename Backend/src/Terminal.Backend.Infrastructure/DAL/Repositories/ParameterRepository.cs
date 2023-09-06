using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ParameterRepository : IParameterRepository
{
    private readonly DbSet<Parameter> _parameters;

    public ParameterRepository(TerminalDbContext dbContext)
    {
        _parameters = dbContext.Parameters;
    }

    public async Task<T?> GetAsync<T>(ParameterName name, CancellationToken ct)
    where T : Parameter
        => await _parameters.SingleOrDefaultAsync(p => p.Name == name, ct) as T;
    
    public async Task AddAsync(Parameter parameter, CancellationToken ct)
        => await _parameters.AddAsync(parameter, ct);

    public Task UpdateAsync(Parameter parameter)
    {
        _parameters.Update(parameter);
        return Task.CompletedTask;
    }
}
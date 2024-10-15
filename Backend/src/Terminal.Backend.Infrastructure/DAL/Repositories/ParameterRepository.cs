using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ParameterRepository(TerminalDbContext dbContext) : IParameterRepository
{
    private readonly DbSet<Parameter> _parameters = dbContext.Parameters;

    public async Task<T?> GetAsync<T>(ParameterId id, CancellationToken cancellationToken)
        where T : Parameter
        => await _parameters
            .Include(p => p.Parent)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken) as T;

    public async Task AddAsync(Parameter parameter, CancellationToken cancellationToken)
        => await _parameters.AddAsync(parameter, cancellationToken);

    public Task UpdateAsync(Parameter parameter)
    {
        _parameters.Update(parameter);
        return Task.CompletedTask;
    }
}

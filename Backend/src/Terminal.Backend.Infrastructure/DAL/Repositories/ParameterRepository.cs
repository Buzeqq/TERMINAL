using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ParameterRepository : IParameterRepository
{
    private readonly DbSet<TextParameter> _textParameters;
    private readonly DbSet<IntegerParameter> _integerParameters;
    private readonly DbSet<DecimalParameter> _decimalParameters;
    private readonly DbSet<Parameter> _parameters;


    public ParameterRepository(TerminalDbContext dbContext)
    {
        _textParameters = dbContext.TextParameters;
        _integerParameters = dbContext.IntegerParameters;
        _decimalParameters = dbContext.DecimalParameters;
        _parameters = dbContext.Parameters;
    }

    public async Task<Parameter?> GetAsync(ParameterName name, CancellationToken ct)
        => await _parameters.SingleOrDefaultAsync(p => p.Name == name, ct);

    public async Task AddTextAsync(TextParameter parameter, CancellationToken ct)
        => await _textParameters.AddAsync(parameter, ct);

    public async Task AddIntegerAsync(IntegerParameter parameter, CancellationToken ct)
        => await _integerParameters.AddAsync(parameter, ct);

    public async Task AddDecimalAsync(DecimalParameter parameter, CancellationToken ct) 
        => await _decimalParameters.AddAsync(parameter, ct);

    public Task UpdateAsync(Parameter parameter)
    {
        _parameters.Update(parameter);
        return Task.CompletedTask;
    }
}
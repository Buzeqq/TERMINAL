using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ParameterRepository : IParameterRepository
{
    private readonly DbSet<TextParameter> _textParameters;
    private readonly DbSet<IntegerParameter> _integerParameters;
    private readonly DbSet<DecimalParameter> _decimalParameters;


    public ParameterRepository(TerminalDbContext dbContext)
    {
        _textParameters = dbContext.TextParameters;
        _integerParameters = dbContext.IntegerParameters;
        _decimalParameters = dbContext.DecimalParameters;
    }

    public async Task AddTextAsync(TextParameter parameter, CancellationToken ct)
        => await _textParameters.AddAsync(parameter, ct);

    public async Task AddIntegerAsync(IntegerParameter parameter, CancellationToken ct)
        => await _integerParameters.AddAsync(parameter, ct);

    public async Task AddDecimalAsync(DecimalParameter parameter, CancellationToken ct) 
        => await _decimalParameters.AddAsync(parameter, ct);
}
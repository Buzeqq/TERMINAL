using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetParameterQueryHandler : IQueryHandler<GetParameterQuery, Parameter?>
{
    private readonly DbSet<Parameter> _parameters;

    public GetParameterQueryHandler(TerminalDbContext dbContext)
    {
        _parameters = dbContext.Parameters;
    }
    
    public async Task<Parameter?> HandleAsync(GetParameterQuery query, CancellationToken ct)
    {
        var name = query.Name;
        return await _parameters.SingleOrDefaultAsync(p => p.Name == name, ct);
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetParameterQueryHandler : IRequestHandler<GetParameterQuery, Parameter?>
{
    private readonly DbSet<Parameter> _parameters;

    public GetParameterQueryHandler(TerminalDbContext dbContext)
    {
        _parameters = dbContext.Parameters;
    }
    
    public async Task<Parameter?> Handle(GetParameterQuery query, CancellationToken ct)
    {
        var id = query.Id;
        return await _parameters.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, ct);
    }
}
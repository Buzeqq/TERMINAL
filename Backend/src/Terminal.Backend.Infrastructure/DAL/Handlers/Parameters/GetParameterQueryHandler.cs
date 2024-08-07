using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Parameters.Get;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Parameters;

internal sealed class GetParameterQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetParameterQuery, Parameter?>
{
    private readonly DbSet<Parameter> _parameters = dbContext.Parameters;

    public Task<Parameter?> Handle(GetParameterQuery query, CancellationToken ct)
    {
        var id = query.Id;
        return _parameters.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, ct);
    }
}
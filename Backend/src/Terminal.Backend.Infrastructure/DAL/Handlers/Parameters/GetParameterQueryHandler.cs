using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Parameters.Get;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Parameters;

internal sealed class GetParameterQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetParameterQuery, Parameter?>
{

    public Task<Parameter?> Handle(GetParameterQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id;
        return dbContext.Parameters
            .TagWith("Get parameter by id")
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}

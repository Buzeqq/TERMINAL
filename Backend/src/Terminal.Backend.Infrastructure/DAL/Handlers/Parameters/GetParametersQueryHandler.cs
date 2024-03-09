using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Parameters;
using Terminal.Backend.Application.Parameters.Get;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Parameters;

internal sealed class GetParametersQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetParametersQuery, GetParametersDto>
{
    private readonly DbSet<Parameter> _parameters = dbContext.Parameters;

    public async Task<GetParametersDto> Handle(GetParametersQuery request, CancellationToken ct)
        => (await _parameters
                .AsNoTracking()
                .Include(p => p.Parent)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Order)
                .ToListAsync(ct))
            .AsGetParametersDto();
}
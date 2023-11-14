using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetParametersQueryHandler : IRequestHandler<GetParametersQuery, GetParametersDto>
{
    private readonly DbSet<Parameter> _parameters;

    public GetParametersQueryHandler(TerminalDbContext dbContext)
    {
        _parameters = dbContext.Parameters;
    }

    public async Task<GetParametersDto> Handle(GetParametersQuery request, CancellationToken ct) 
        => (await _parameters
                .AsNoTracking()
                .Where(p => p.IsActive)
                .ToListAsync(ct))
            .AsGetParametersDto();
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Queries.Samples.Get;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Samples;

internal class GetSampleQueryHandler : IRequestHandler<GetSampleQuery, GetSampleDto?>
{
    private readonly TerminalDbContext _dbContext;

    public GetSampleQueryHandler(TerminalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetSampleDto?> Handle(GetSampleQuery request, CancellationToken ct)
    {
        var sample = await _dbContext.Samples
            .AsNoTracking()
            .Include(s => s.Project)
            .Include(s => s.Recipe)
            .Include(s => s.Steps)
            .ThenInclude(s => s.Parameters)
            .ThenInclude(p => p.Parameter)
            .Include(s => s.Tags)
            .SingleOrDefaultAsync(s => s.Id.Equals(request.Id), ct);

        return sample?.AsGetSampleDto();
    }
}
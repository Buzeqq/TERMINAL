using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Samples.Get;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Samples;

internal class GetSampleQueryHandler(TerminalDbContext dbContext) : IRequestHandler<GetSampleQuery, GetSampleDto?>
{
    public async Task<GetSampleDto?> Handle(GetSampleQuery request, CancellationToken ct)
    {
        var sample = await dbContext.Samples
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
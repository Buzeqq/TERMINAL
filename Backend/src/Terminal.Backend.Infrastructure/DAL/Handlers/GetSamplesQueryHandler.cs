using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Samples.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetSamplesQueryHandler : IRequestHandler<GetSamplesQuery, GetSamplesDto>
{
    private readonly DbSet<Sample> _samples;

    public GetSamplesQueryHandler(TerminalDbContext dbContext)
    {
        _samples = dbContext.Samples;
    }
    
    public async Task<GetSamplesDto> Handle(GetSamplesQuery request, CancellationToken ct)
    {
        var samples = await _samples
            .AsNoTracking()
            .OrderByDescending(m => m.CreatedAtUtc)
            .Include(m => m.Project)
            .Include(m => m.Tags)
            .Paginate(request.Parameters)
            .Select(m => new GetSamplesDto.SampleDto(
                m.Id, m.Code.Value, m.Project.Name, m.CreatedAtUtc.ToString("o"), m.Comment))
            .ToListAsync(ct);

        return new GetSamplesDto { Samples = samples };
    }
}
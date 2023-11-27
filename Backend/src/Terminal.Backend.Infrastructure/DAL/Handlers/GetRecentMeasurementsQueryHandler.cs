using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Samples.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetRecentMeasurementsQueryHandler : 
    IRequestHandler<GetRecentSamplesQuery, GetRecentSamplesDto>
{
    private readonly DbSet<Sample> _measurements;

    public GetRecentMeasurementsQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task<GetRecentSamplesDto> Handle(GetRecentSamplesQuery request,
        CancellationToken cancellationToken)
        => new()
        {
            RecentSamples = await _measurements
                .OrderByDescending(m => m.CreatedAtUtc)
                .Take(request.Length)
                .Select(m => new GetSamplesDto.SampleDto(m.Id, m.Code.Value, m.Project.Name, m.CreatedAtUtc.ToString("o"), m.Comment))
                .ToListAsync(cancellationToken)
        };
}
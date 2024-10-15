using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Samples.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Samples;

internal sealed class GetRecentSamplesQueryHandler(TerminalDbContext dbContext) :
    IRequestHandler<GetRecentSamplesQuery, GetRecentSamplesDto>
{
    private readonly DbSet<Sample> _samples = dbContext.Samples;

    public async Task<GetRecentSamplesDto> Handle(GetRecentSamplesQuery request,
        CancellationToken cancellationToken)
    {
        var samples = await _samples
            .TagWith("Get Recent Samples")
            .OrderByDescending(m => m.CreatedAtUtc)
            .Take(request.Length)
            .Select(s =>
                new GetSamplesDto.SampleDto(
                    s.Id,
                    s.Code.Value,
                    s.Project.Name,
                    s.Recipe != null ? s.Recipe.Name.Value : null,
                    s.CreatedAtUtc.ToString("o"),
                    s.Comment))
            .ToListAsync(cancellationToken);

        return new GetRecentSamplesDto(samples);
    }
}

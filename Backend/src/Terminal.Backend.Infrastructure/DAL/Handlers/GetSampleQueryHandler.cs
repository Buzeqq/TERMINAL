using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Samples.Get;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;
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
            .Include(m => m.Project)
            .Include(m => m.Recipe)
            // FIXME: .Include(m => m.Steps)
            // FIXME: .Include(m => m.Tags)
            .Select(m => new GetSampleDto
            {
                Code = m.Code.Value,
                Comment = m.Comment,
                CreatedAtUtc = m.CreatedAtUtc.ToString("o"),
                Id = m.Id,
                ProjectId = m.Project.Id,
                RecipeId = m.Recipe!.Id
            })
            .SingleOrDefaultAsync(m => m.Id == request.Id, ct);
        if (sample is null) return sample;
        
        var tags = await _dbContext.Samples
            .AsNoTracking()
            .Where(m => m.Id.Equals(request.Id))
            .SelectMany(m => m.Tags)
            .Select(t => t.Name)
            .ToListAsync(ct);
        var steps = await _dbContext.Samples
            .AsNoTracking()
            .Where(m => m.Id.Equals(request.Id))
            .SelectMany(m => m.Steps)
            .Include(s => s.Parameters)
            .ThenInclude(p => p.Parameter)
            .ToListAsync(ct);
        
        sample.Tags = tags.Select(t => t.Value);
        sample.Steps = steps.AsStepsDto();
        return sample;
    }
}
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
            .Include(s => s.Project)
            .Include(s => s.Recipe)
            .Include(s => s.Steps) // FIXME
            .Include(s => s.Tags) // FIXME
            .Select(s => new GetSampleDto
            {
                Code = s.Code.Value,
                Comment = s.Comment,
                CreatedAtUtc = s.CreatedAtUtc.ToString("o"),
                Id = s.Id,
                ProjectId = s.Project.Id,
                RecipeId = s.Recipe!.Id
            })
            .SingleOrDefaultAsync(s => s.Id == request.Id, ct);
        if (sample is null) return sample;
        
        var tags = await _dbContext.Samples
            .AsNoTracking()
            .Where(s => s.Id.Equals(request.Id))
            .SelectMany(s => s.Tags)
            .Select(t => t.Name)
            .ToListAsync(ct);
        var steps = await _dbContext.Samples
            .AsNoTracking()
            .Where(s => s.Id.Equals(request.Id))
            .SelectMany(s => s.Steps)
            .Include(s => s.Parameters)
            .ThenInclude(p => p.Parameter)
            .ToListAsync(ct);
        
        sample.Tags = tags.Select(t => t.Value);
        sample.Steps = steps.AsStepsDto();
        return sample;
    }
}
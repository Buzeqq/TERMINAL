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
            .SingleOrDefaultAsync(s => s.Id.Equals(request.Id), ct);
        if (sample is null) return null;

        GetRecipeDto? recipeDto = null;
        if (sample is { Recipe: not null })
        {
            recipeDto = sample.Recipe.AsDto();
        }

        var dto = new GetSampleDto(sample.Id, sample.Code, recipeDto, sample.CreatedAtUtc.ToString("o"), sample.Comment,
            sample.Project.Id, sample.Steps.AsStepsDto(), sample.Tags.Select(t => t.Name.Value));
        
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
        
        dto.Tags = tags.Select(t => t.Value);
        dto.Steps = steps.AsStepsDto();
        return dto;
    }
}
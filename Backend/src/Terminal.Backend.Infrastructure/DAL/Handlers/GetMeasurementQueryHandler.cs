using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;
internal class GetMeasurementQueryHandler : IRequestHandler<GetMeasurementQuery, GetMeasurementDto?>
{
    private readonly TerminalDbContext _dbContext;
    public GetMeasurementQueryHandler(TerminalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetMeasurementDto?> Handle(GetMeasurementQuery request, CancellationToken ct)
    {
        var measurement = await _dbContext.Measurements
            .AsNoTracking()
            .Include(m => m.Project)
            .Include(m => m.Recipe)
            // FIXME: .Include(m => m.Steps)
            // FIXME: .Include(m => m.Tags)
            .Select(m => new GetMeasurementDto
            {
                Code = m.Code.Value,
                Comment = m.Comment,
                CreatedAtUtc = m.CreatedAtUtc.ToString("o"),
                Id = m.Id,
                ProjectId = m.Project.Id,
                RecipeId = m.Recipe!.Id
            })
            .SingleOrDefaultAsync(m => m.Id == request.Id, ct);
        if (measurement is null) return measurement;
        
        var tags = await _dbContext.Measurements
            .AsNoTracking()
            .Where(m => m.Id == request.Id)
            .SelectMany(m => m.Tags)
            .Select(t => t.Name.Value)
            .ToListAsync(ct);
        var stepIds = await _dbContext.Measurements
            .AsNoTracking()
            .Where(m => m.Id == request.Id)
            .SelectMany(m => m.Steps)
            .Select(s => s.Id.Value)
            .ToListAsync(ct);
        
        measurement.Tags = tags;
        measurement.StepIds = stepIds;
        return measurement;
    }
}
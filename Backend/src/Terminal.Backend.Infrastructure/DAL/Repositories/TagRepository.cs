using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class TagRepository : ITagRepository
{
    private readonly DbSet<Tag> _tags;

    public TagRepository(TerminalDbContext dbContext)
    {
        _tags = dbContext.Tags;
    }

    public async Task AddAsync(Tag tag, CancellationToken ct)
        => await _tags.AddAsync(tag, ct);

    public async Task<Tag?> GetAsync(TagId id, CancellationToken ct)
        => await _tags.SingleOrDefaultAsync(t => t.Id == id, ct);

    public Task UpdateAsync(Tag tag)
    {
        _tags.Update(tag);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagId> tagIds, CancellationToken ct)
        => await _tags.Where(t => tagIds.Equals(t.Id)).ToListAsync(ct);
}
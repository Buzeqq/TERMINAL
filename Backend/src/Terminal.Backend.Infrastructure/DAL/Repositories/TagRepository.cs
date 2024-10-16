using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class TagRepository(TerminalDbContext dbContext) : ITagRepository
{
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    public async Task AddAsync(Tag tag, CancellationToken cancellationToken)
        => await _tags.AddAsync(tag, cancellationToken);

    public Task<Tag?> GetAsync(TagId id, CancellationToken cancellationToken)
        =>
            _tags.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

    public Task UpdateAsync(Tag tag, CancellationToken cancellationToken)
    {
        _tags.Update(tag);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagId> tagIds, CancellationToken cancellationToken)
        => await _tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

    public Task DeleteAsync(Tag tag, CancellationToken cancellationToken)
    {
        _tags.Remove(tag);
        return Task.CompletedTask;
    }

    public Task<bool> IsNameUniqueAsync(TagName name, CancellationToken cancellationToken)
        =>
            _tags.AllAsync(t => t.Name != name, cancellationToken);
}

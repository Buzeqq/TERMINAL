using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class TagRepository(TerminalDbContext dbContext) : ITagRepository
{
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    public async Task AddAsync(Tag tag, CancellationToken ct)
        => await this._tags.AddAsync(tag, ct);

    public Task<Tag?> GetAsync(TagId id, CancellationToken ct)
        =>
            this._tags.SingleOrDefaultAsync(t => t.Id == id, ct);

    public Task UpdateAsync(Tag tag, CancellationToken ct)
    {
        this._tags.Update(tag);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagId> tagIds, CancellationToken ct) 
        => await this._tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync(ct);

    public Task DeleteAsync(Tag tag, CancellationToken cancellationToken)
    {
        this._tags.Remove(tag);
        return Task.CompletedTask;
    }

    public Task<bool> IsNameUniqueAsync(TagName name, CancellationToken cancellationToken) 
        =>
            this._tags.AllAsync(t => t.Name != name, cancellationToken);
}
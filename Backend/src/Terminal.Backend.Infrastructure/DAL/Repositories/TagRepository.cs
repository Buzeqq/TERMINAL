using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
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

    public async Task<Tag?> GetAsync(TagName name, CancellationToken ct)
        => await _tags.SingleOrDefaultAsync(t => t.Name == name, ct);

    public Task UpdateAsync(Tag tag)
    {
        _tags.Update(tag);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagName> tagNames, CancellationToken ct)
        => await _tags.Where(t => tagNames.Contains(t.Name)).ToListAsync(ct);
}
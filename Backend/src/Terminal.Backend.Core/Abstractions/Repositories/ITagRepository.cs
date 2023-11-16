using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface ITagRepository
{
    Task AddAsync(Tag tag, CancellationToken ct);
    Task<Tag?> GetAsync(TagName name, CancellationToken ct);
    Task UpdateAsync(Tag tag);
    Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagName> tagNames, CancellationToken ct);
}
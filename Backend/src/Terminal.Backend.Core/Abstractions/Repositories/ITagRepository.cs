using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface ITagRepository
{
    Task AddAsync(Tag tag, CancellationToken ct);
    Task<Tag?> GetAsync(TagId id, CancellationToken ct);
    Task UpdateAsync(Tag tag, CancellationToken ct);
    Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagId> tagIds, CancellationToken ct);
    Task DeleteAsync(Tag tag, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(TagName name, CancellationToken cancellationToken);
}
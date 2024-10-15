using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface ITagRepository
{
    Task AddAsync(Tag tag, CancellationToken cancellationToken);
    Task<Tag?> GetAsync(TagId id, CancellationToken cancellationToken);
    Task UpdateAsync(Tag tag, CancellationToken cancellationToken);
    Task<IEnumerable<Tag>> GetManyAsync(IEnumerable<TagId> tagIds, CancellationToken cancellationToken);
    Task DeleteAsync(Tag tag, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(TagName name, CancellationToken cancellationToken);
}

using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Tags.Update;

internal sealed class UpdateTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<UpdateTagCommand>
{
    public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var (id, name) = request;
        var tag = await tagRepository.GetAsync(id, cancellationToken);
        if (tag is null)
        {
            throw new TagNotFoundException();
        }

        if (tag.Name != request.Name && !await tagRepository.IsNameUniqueAsync(name, cancellationToken))
        {
            throw new InvalidTagException(name);
        }

        tag.Update(name);
        await tagRepository.UpdateAsync(tag, cancellationToken);
    }
}

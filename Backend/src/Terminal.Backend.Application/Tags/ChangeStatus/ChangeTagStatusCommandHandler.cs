using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Tags.ChangeStatus;

internal sealed class ChangeTagStatusCommandHandler(ITagRepository tagRepository)
    : IRequestHandler<ChangeTagStatusCommand>
{
    public async Task Handle(ChangeTagStatusCommand command, CancellationToken cancellationToken)
    {
        var (id, status) = command;
        var tag = await tagRepository.GetAsync(id, cancellationToken);
        if (tag is null)
        {
            throw new TagNotFoundException();
        }

        tag.ChangeStatus(status);
        await tagRepository.UpdateAsync(tag, cancellationToken);
    }
}

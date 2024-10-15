using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Tags.Delete;

internal sealed class DeleteTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<DeleteTagCommand>
{
    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var tag = await tagRepository.GetAsync(id, cancellationToken);
        if (tag is null)
        {
            throw new TagNotFoundException();
        }

        await tagRepository.DeleteAsync(tag, cancellationToken);
    }
}

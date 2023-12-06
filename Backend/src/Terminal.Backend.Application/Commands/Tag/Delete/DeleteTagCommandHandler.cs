using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Commands.Tag.Delete;

internal sealed class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly ITagRepository _tagRepository;

    public DeleteTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var tag = await _tagRepository.GetAsync(id, cancellationToken);
        if (tag is null)
        {
            throw new TagNotFoundException();
        }

        await _tagRepository.DeleteAsync(tag, cancellationToken);
    }
}
using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Commands.Tag.Update;

internal sealed class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
{
    private readonly ITagRepository _tagRepository;

    public UpdateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var (id, name) = request;
        var tag = await _tagRepository.GetAsync(id, cancellationToken);
        if (tag is null)
        {
            throw new TagNotFoundException();
        }

        tag.Update(name);
        await _tagRepository.UpdateAsync(tag, cancellationToken);
    }
}
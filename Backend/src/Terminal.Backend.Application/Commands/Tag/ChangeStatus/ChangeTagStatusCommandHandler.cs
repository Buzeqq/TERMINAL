using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Commands.Tag.ChangeStatus;

internal sealed class ChangeTagStatusCommandHandler : IRequestHandler<ChangeTagStatusCommand>
{
    private readonly ITagRepository _tagRepository;

    public ChangeTagStatusCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }


    public async Task Handle(ChangeTagStatusCommand command, CancellationToken ct)
    {
        var (name, status) = command;
        var tag = await _tagRepository.GetAsync(name, ct);
        if (tag is null)
        {
            throw new TagNotFoundException(name);
        }

        tag.ChangeStatus(status);
        await _tagRepository.UpdateAsync(tag);
    }
}
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

public sealed class ChangeTagStatusCommandHandler : ICommandHandler<ChangeTagStatusCommand>
{
    private readonly ITagRepository _tagRepository;

    public ChangeTagStatusCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }


    public async Task HandleAsync(ChangeTagStatusCommand command, CancellationToken ct)
    {
        var (name, status) = command;
        var tag = await _tagRepository.GetAsync(name, ct);
        if (tag is null)
        {
            return;
        }

        tag.ChangeStatus(status);
        _tagRepository.UpdateAsync(tag);
    }
}
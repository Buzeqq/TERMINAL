using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

public sealed class CreateTagCommandHandler : ICommandHandler<CreateTagCommand>
{
    private readonly ITagRepository _tagRepository;

    public CreateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task HandleAsync(CreateTagCommand command, CancellationToken ct)
    {
        var name = command.Name;
        var newTag = new Tag(name);
        
        await _tagRepository.AddAsync(newTag, ct);
    }
}
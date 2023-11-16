using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Tag.Create;

internal sealed class CreateTagCommandHandler : IRequestHandler<CreateTagCommand>
{
    private readonly ITagRepository _tagRepository;

    public CreateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task Handle(CreateTagCommand command, CancellationToken ct)
    {
        var name = command.Name;
        var newTag = new Core.Entities.Tag(name);
        
        await _tagRepository.AddAsync(newTag, ct);
    }
}
using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Tag.Create;

internal sealed class CreateTagCommandHandler : IRequestHandler<CreateTagCommand>
{
    private readonly ITagRepository _tagRepository;

    public CreateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public Task Handle(CreateTagCommand command, CancellationToken ct)
    {
        var newTagId = TagId.Create();
        var newTag = new Core.Entities.Tag(newTagId, command.Name);
        return _tagRepository.AddAsync(newTag, ct);
    }
}
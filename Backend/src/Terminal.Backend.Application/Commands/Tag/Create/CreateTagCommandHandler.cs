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

    public Task Handle(CreateTagCommand command, CancellationToken ct)
    {
        var newTag = new Core.Entities.Tag(command.Id, command.Name);
        return _tagRepository.AddAsync(newTag, ct);
    }
}
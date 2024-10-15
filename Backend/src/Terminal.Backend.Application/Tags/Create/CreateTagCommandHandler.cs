using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Tags.Create;

internal sealed class CreateTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<CreateTagCommand>
{
    public Task Handle(CreateTagCommand command, CancellationToken cancellationToken)
    {
        var newTag = new Core.Entities.Tag(command.Id, command.Name);
        return tagRepository.AddAsync(newTag, cancellationToken);
    }
}

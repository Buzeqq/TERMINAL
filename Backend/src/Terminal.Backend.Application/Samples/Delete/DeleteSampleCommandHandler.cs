using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Samples.Delete;

internal sealed class DeleteSampleCommandHandler(ISampleRepository sampleRepository)
    : IRequestHandler<DeleteSampleCommand>
{
    public async Task Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        if (await sampleRepository.ExistsAsync(id, cancellationToken))
        {
            throw new SampleNotFoundException();
        }

        await sampleRepository.DeleteAsync(id, cancellationToken);
    }
}

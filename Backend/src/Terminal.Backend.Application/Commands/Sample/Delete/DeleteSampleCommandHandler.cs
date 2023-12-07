using MediatR;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Sample.Delete;

internal sealed class DeleteSampleCommandHandler : IRequestHandler<DeleteSampleCommand>
{
    private readonly ISampleRepository _sampleRepository;

    public DeleteSampleCommandHandler(ISampleRepository sampleRepository)
    {
        _sampleRepository = sampleRepository;
    }

    public async Task Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sample = await _sampleRepository.GetAsync(id, cancellationToken);
        if (sample is null)
        {
            throw new SampleNotFoundException();
        }

        await _sampleRepository.DeleteAsync(sample, cancellationToken);
    }
}
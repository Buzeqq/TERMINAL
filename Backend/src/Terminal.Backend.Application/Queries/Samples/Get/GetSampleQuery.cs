using MediatR;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Queries.Samples.Get;

public class GetSampleQuery : IRequest<GetSampleDto?>
{
    public Guid Id { get; set; }
}
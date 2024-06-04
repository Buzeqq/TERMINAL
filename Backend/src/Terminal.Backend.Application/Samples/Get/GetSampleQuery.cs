using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Samples.Get;

public class GetSampleQuery : IRequest<GetSampleDto?>
{
    public Guid Id { get; set; }
}
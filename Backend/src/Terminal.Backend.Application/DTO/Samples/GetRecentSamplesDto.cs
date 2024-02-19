namespace Terminal.Backend.Application.DTO.Samples;

public class GetRecentSamplesDto
{
    public IEnumerable<GetSamplesDto.SampleDto> RecentSamples { get; set; } =
        Enumerable.Empty<GetSamplesDto.SampleDto>();
}
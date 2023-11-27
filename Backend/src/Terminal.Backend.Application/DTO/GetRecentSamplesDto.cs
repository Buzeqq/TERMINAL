namespace Terminal.Backend.Application.DTO;

public class GetRecentSamplesDto
{
    public IEnumerable<GetSamplesDto.SampleDto> RecentSamples { get; set; }
}
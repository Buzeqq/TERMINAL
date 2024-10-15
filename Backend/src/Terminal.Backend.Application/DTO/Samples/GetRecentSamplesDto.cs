namespace Terminal.Backend.Application.DTO.Samples;

public record GetRecentSamplesDto(IEnumerable<GetSamplesDto.SampleDto> RecentSamples);

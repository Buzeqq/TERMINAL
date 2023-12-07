namespace Terminal.Backend.Application.DTO.Samples;

public class GetSamplesDto
{
    public IEnumerable<SampleDto> Samples { get; set; }

    public sealed record SampleDto(Guid Id, string Code, string Project, string CreatedAtUtc, string Comment);
}
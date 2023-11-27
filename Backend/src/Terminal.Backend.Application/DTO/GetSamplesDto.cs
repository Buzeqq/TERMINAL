namespace Terminal.Backend.Application.DTO;

public class GetSamplesDto
{
    public IEnumerable<SampleDto> Samples { get; set; }
    
    public sealed record SampleDto(Guid Id, string Code, string Project, string CreatedAtUtc, string Comment);
}
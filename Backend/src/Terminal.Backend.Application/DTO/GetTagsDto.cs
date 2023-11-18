namespace Terminal.Backend.Application.DTO;

public class GetTagsDto
{
    public IEnumerable<TagDto> Tags { get; set; }

    public record TagDto(Guid id, string name);
}
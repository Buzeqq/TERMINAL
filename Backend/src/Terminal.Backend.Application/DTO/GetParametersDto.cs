namespace Terminal.Backend.Application.DTO;

public class GetParametersDto
{
    public IEnumerable<ParameterDto> Parameters { get; set; }

    public record ParameterDto(Guid Id, string Name);
}
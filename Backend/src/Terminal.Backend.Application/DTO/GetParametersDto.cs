using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.DTO;

public class GetParametersDto
{
    public IEnumerable<Parameter> Parameters { get; set; }
}
namespace Terminal.Backend.Application.DTO;

public class GetMeasurementsDto
{
    public IEnumerable<MeasurementDto> Measurements { get; set; }
    
    public sealed record MeasurementDto(Guid Id, string Code, string Project, string CreatedAtUtc);
}
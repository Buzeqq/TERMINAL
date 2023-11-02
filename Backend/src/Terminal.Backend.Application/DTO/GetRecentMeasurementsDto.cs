namespace Terminal.Backend.Application.DTO;

public class GetRecentMeasurementsDto
{
    public IEnumerable<GetMeasurementsDto.MeasurementDto> RecentMeasurements { get; set; }
}
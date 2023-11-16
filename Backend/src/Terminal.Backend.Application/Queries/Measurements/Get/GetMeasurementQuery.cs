using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries.Measurements.Get;

public class GetMeasurementQuery : IRequest<GetMeasurementDto?>
{
    public Guid Id { get; set; }
}
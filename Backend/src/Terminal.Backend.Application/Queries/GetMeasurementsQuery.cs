using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters;

namespace Terminal.Backend.Application.Queries;

public sealed class GetMeasurementsQuery : IRequest<GetMeasurementsDto>
{
    public PagingParameters Parameters { get; set; }
}
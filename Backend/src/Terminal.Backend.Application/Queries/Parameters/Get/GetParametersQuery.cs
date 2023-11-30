using MediatR;
using Terminal.Backend.Application.DTO.Parameters;

namespace Terminal.Backend.Application.Queries.Parameters.Get;

public sealed class GetParametersQuery : IRequest<GetParametersDto>
{
}
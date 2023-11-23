using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries.Parameters.Get;

public sealed class GetParametersQuery : IRequest<GetParametersDto>
{
}
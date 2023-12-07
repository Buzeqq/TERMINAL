using MediatR;
using Terminal.Backend.Application.DTO.Users;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Queries.Users;

public sealed class GetUserQuery : IRequest<GetUserDto?>
{
    public UserId Id { get; init; }

    public GetUserQuery(Guid id)
    {
        Id = id;
    }
}
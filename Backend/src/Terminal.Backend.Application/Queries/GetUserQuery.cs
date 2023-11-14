using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Queries;

public sealed class GetUserQuery : IRequest<GetUserDto?>
{
    public UserId Id { get; init; }
    public GetUserQuery(Guid id)
    {
        Id = id;
    }
}

public record GetUserDto(string Email, string Token)
{
}
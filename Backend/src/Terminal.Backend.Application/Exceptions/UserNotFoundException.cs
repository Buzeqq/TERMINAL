using System.Net;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class UserNotFoundException() : TerminalException("User not found")
{
    public override HttpStatusCode? StatusCode { get; init; } = HttpStatusCode.NotFound;
}
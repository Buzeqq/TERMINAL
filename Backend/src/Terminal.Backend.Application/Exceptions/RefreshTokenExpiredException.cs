using System.Net;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class RefreshTokenExpiredException(DateTimeOffset? expiredSince) 
    : TerminalException("Refresh token expired", expiredSince is not null ? 
        $"The refresh token provided expired {expiredSince}." :
        string.Empty)
{
    public override HttpStatusCode? StatusCode { get; init; } = HttpStatusCode.Unauthorized;
}
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Infrastructure;

internal sealed class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception is TerminalException terminalException
            ? (terminalException.StatusCode as int? ?? StatusCodes.Status400BadRequest, terminalException.Message)
            : (StatusCodes.Status500InternalServerError, "An unexpected error occured");

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.Headers.ContentType = "application/problem+json; charset=utf-8";
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Type = Defaults[statusCode].Type,
            Status = statusCode,
            Title = title,
            Instance = httpContext.Request.Path,
            Detail = (exception as TerminalException)?.Details ?? string.Empty,
            Extensions =
            {
                ["errors"] = (exception as TerminalException)?.Errors ?? []
            }
        }, cancellationToken);

        return true;
    }

    private static readonly Dictionary<int, (string Type, string Title)> Defaults = new()
    {
        [400] = ("https://tools.ietf.org/html/rfc9110#section-15.5.1", "Bad Request"),
        [401] = ("https://tools.ietf.org/html/rfc9110#section-15.5.2", "Unauthorized"),
        [403] = ("https://tools.ietf.org/html/rfc9110#section-15.5.4", "Forbidden"),
        [404] = ("https://tools.ietf.org/html/rfc9110#section-15.5.5", "Not Found"),
        [405] = ("https://tools.ietf.org/html/rfc9110#section-15.5.6", "Method Not Allowed"),
        [406] = ("https://tools.ietf.org/html/rfc9110#section-15.5.7", "Not Acceptable"),
        [408] = ("https://tools.ietf.org/html/rfc9110#section-15.5.9", "Request Timeout"),
        [409] = ("https://tools.ietf.org/html/rfc9110#section-15.5.10", "Conflict"),
        [412] = ("https://tools.ietf.org/html/rfc9110#section-15.5.13", "Precondition Failed"),
        [415] = ("https://tools.ietf.org/html/rfc9110#section-15.5.16", "Unsupported Media Type"),
        [422] = ("https://tools.ietf.org/html/rfc4918#section-11.2", "Unprocessable Entity"),
        [426] = ("https://tools.ietf.org/html/rfc9110#section-15.5.22", "Upgrade Required"),
        [500] = ("https://tools.ietf.org/html/rfc9110#section-15.6.1", "An error occurred while processing your request."),
        [502] = ("https://tools.ietf.org/html/rfc9110#section-15.6.3", "Bad Gateway"),
        [503] = ("https://tools.ietf.org/html/rfc9110#section-15.6.4", "Service Unavailable"),
        [504] = ("https://tools.ietf.org/html/rfc9110#section-15.6.5", "Gateway Timeout")
    };

}

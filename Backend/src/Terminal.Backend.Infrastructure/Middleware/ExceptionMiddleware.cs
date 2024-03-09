using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Infrastructure.Middleware;

internal sealed class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "{Message}", exception.Message);
            await HandleErrorAsync(context, exception);
        }
    }

    private static Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var (statusCode, error) = exception switch
        {
            TerminalException => (StatusCodes.Status400BadRequest,
                new Error(exception.GetType().Name.Replace("_exception", string.Empty), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsJsonAsync(error);
    }

    private record Error(string Code, string Reason);
}
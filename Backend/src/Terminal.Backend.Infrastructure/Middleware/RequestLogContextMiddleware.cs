using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Terminal.Backend.Infrastructure.Middleware;

internal sealed class RequestLogContextMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return next(context);
        }
    }
}

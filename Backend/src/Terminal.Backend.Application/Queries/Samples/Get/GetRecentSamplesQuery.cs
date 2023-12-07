using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Queries.Samples.Get;

public sealed record GetRecentSamplesQuery(int Length) : IRequest<GetRecentSamplesDto>
{
    public static ValueTask<GetRecentSamplesQuery?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        const string numberKey = "len";
        var parsed = int.TryParse(context.Request.Query[numberKey], out var length);
        length = parsed ? length : 5;

        var result = new GetRecentSamplesQuery(length);

        return ValueTask.FromResult<GetRecentSamplesQuery?>(result);
    }
}
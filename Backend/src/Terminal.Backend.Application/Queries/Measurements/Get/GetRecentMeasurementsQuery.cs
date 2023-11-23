using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries.Measurements.Get;

public sealed record GetRecentMeasurementsQuery(int Length) : IRequest<GetRecentMeasurementsDto>
{
    public static ValueTask<GetRecentMeasurementsQuery?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        const string numberKey = "len";
        var parsed = int.TryParse(context.Request.Query[numberKey], out var length);
        length = parsed ? length : 5;
        
        var result = new GetRecentMeasurementsQuery(length);

        return ValueTask.FromResult<GetRecentMeasurementsQuery?>(result);
    }
}


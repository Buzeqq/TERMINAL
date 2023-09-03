using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Commands;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Api.Modules;

public static class MeasurementsModule
{
    public static void UseMeasurementsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/measurements", async (
            CreateMeasurementCommand command,
            ICommandHandler<CreateMeasurementCommand> handler,
        CancellationToken ct) =>
        {
            Console.WriteLine(command);
        });

        app.MapGet("api/measurements/example", async () =>
        {
            var measurement = new CreateMeasurementCommand(null, new []
            {
                new CreateMeasurementStepDto(new CreateMeasurementBaseParameterValueDto[]
                {
                    new CreateMeasurementDecimalParameterValueDto("decimal", 0.111m),
                    new CreateMeasurementIntegerParameterValueDto("integer", 2137),
                    new CreateMeasurementTextParameterValueDto("text", "text")
                },
                    "comment")
            },
            new []
            {
                "tag1", "tag2", "tag3"
            },
            "comment");

            return Results.Ok(measurement);
        });
    }
}
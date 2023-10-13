using MediatR;
using Terminal.Backend.Application.Commands.Measurement.Create;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class MeasurementsModule
{
    public static void UseMeasurementsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/measurements", async (
            CreateMeasurementCommand command,
            ISender sender,
        CancellationToken ct) =>
        {
            var id = MeasurementId.Create();
            command = command with { MeasurementId = id };
            await sender.Send(command, ct);
            return Results.Created($"api/measurement/{id}", null);
        });

        app.MapGet("api/measurements/example", async () =>
        {
            var measurement = new CreateMeasurementCommand(MeasurementId.Create(), null, new []
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
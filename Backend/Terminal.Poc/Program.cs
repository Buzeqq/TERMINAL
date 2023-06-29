using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Terminal.Poc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseInMemoryDatabase("terminal_measurements");
});
builder.Services.AddScoped<IRepository<Measurement>, MeasurementRepository>();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}

app.UseRouting();

app.MapPost("/api/measurements", async ([FromBody] PostMeasurementRequest measurement, IRepository<Measurement> measurementRepository) =>
{
    var newMeasurement = new Measurement(measurement.Value);
    await measurementRepository.CreateAsync(newMeasurement);
    return Results.Created($"/api/measurements/{newMeasurement.Id}", newMeasurement);
});
app.MapGet("/api/measurements", async (IRepository<Measurement> measurementRepository) => await measurementRepository.GetAllAsync());
app.MapGet("/api/measurements/{id:guid}", async (Guid id, IRepository<Measurement> measurementRepository) => await measurementRepository.FindAsync(id));
app.MapDelete("/api/measurements/{id:guid}",
    async (Guid id, IRepository<Measurement> measurementRepository) =>
    {
        var measurement = await measurementRepository.FindAsync(id);
        if (measurement is null)
        {
            return Results.NotFound();
        }
        
        await measurementRepository.DeleteAsync(measurement);
        return Results.Ok();
    });
app.MapPut("/api/measurements/{id:guid}",
    async ([FromBody] PutMeasurementRequest updateMeasurement, Guid id, IRepository<Measurement> measurementRepository) =>
    {
        var measurement = await measurementRepository.FindAsync(id);
        if (measurement is null)
        {
            return Results.NotFound();
        }

        measurement.Value = updateMeasurement.Value;
        await measurementRepository.UpdateAsync(measurement);
        return Results.Ok();
    });
app.MapGet("/api/ping", () => Results.Ok());

app.Run();
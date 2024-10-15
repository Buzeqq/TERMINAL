using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Terminal.Backend.Api.Identity;
using Terminal.Backend.Api.Parameters;
using Terminal.Backend.Api.Projects;
using Terminal.Backend.Api.Recipes;
using Terminal.Backend.Api.Samples;
using Terminal.Backend.Api.Tags;
using Terminal.Backend.Application;
using Terminal.Backend.Core;
using Terminal.Backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = app.Environment.IsDevelopment() ?
        UIResponseWriter.WriteHealthCheckUIResponse :
        UIResponseWriter.WriteHealthCheckUIResponseNoExceptionDetails
});

app.UseInfrastructure();
app.UseSerilogRequestLogging();

app.MapGroup("api/v1")
    .UseIdentityEndpoints()
    .UseProjectsEndpoints()
    .UseTagEndpoints()
    .UseRecipesEndpoints()
    .UseParametersEndpoints()
    .UseSamplesEndpoints();

app.Run();

#region Program class declaration for testing purposes

namespace Terminal.Backend.Api
{
    // ReSharper disable once PartialTypeWithSinglePart
    // ReSharper disable once ClassNeverInstantiated.Global
    public partial class Program;
}

#endregion

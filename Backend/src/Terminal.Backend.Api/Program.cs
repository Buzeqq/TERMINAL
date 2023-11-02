using Serilog;
using Terminal.Backend.Api.Modules;
using Terminal.Backend.Application;
using Terminal.Backend.Core;
using Terminal.Backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddSwaggerGen();

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
        .WriteTo.Console();
    
    if (context.HostingEnvironment.IsProduction())
    {
        loggerConfiguration
            .WriteTo.File(context.Configuration["LogFile"] ?? "terminal.log");
    }
});

var app = builder.Build();
app.UseInfrastructure();
app.UsePingEndpoints();
app.UseProjectsEndpoints();
app.UseTagEndpoints();
app.UseRecipesEndpoints();
app.UseParametersEndpoints();
app.UseMeasurementsEndpoints();
app.Run();

namespace Terminal.Backend.Api
{
    public class Program { }
}

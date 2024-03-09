using Terminal.Backend.Api.Parameters;
using Terminal.Backend.Api.Projects;
using Terminal.Backend.Api.Recipes;
using Terminal.Backend.Api.Samples;
using Terminal.Backend.Api.Tags;
using Terminal.Backend.Api.Users;
using Terminal.Backend.Application;
using Terminal.Backend.Core;
using Terminal.Backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/api/health");

app.UseIdentityEndpoints();
app.UseInfrastructure();
app.UseProjectsEndpoints();
app.UseTagEndpoints();
app.UseRecipesEndpoints();
app.UseParametersEndpoints();
app.UseSamplesEndpoints();

app.Run();

#region Program class declaration for testing purposes

namespace Terminal.Backend.Api
{
    public class Program;
}

#endregion

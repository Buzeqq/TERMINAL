using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Abstractions.Behaviors;
using Terminal.Backend.Application.Services;

namespace Terminal.Backend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });
        services.AddMapster();
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddScoped<IConvertDtoService, ConvertDtoService>();

        return services;
    }
}
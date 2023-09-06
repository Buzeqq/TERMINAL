using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Services;

namespace Terminal.Backend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(s => s.FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IConvertDtoService, ConvertDtoService>();
        
        return services;
    }
}